using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Helpers;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HP.ClearingCenter.Application.Data.Entities;

namespace HP.ClearingCenter.Application.ProductGroups.QueryHandlers.Builders
{
    public class GetReturnObjectGroupsSqlBuilder
    {
        private ClearingCenterDataContext db;

        public GetReturnObjectGroupsSqlBuilder(ClearingCenterDataContext db)
        {
            this.db = db;
        }

        public IDbCommand BuildCommand(IEnumerable<ProductGroupCategory> categories, GetReturnObjectsByProductGroupCodeQuery args)
        {
            IDbCommand cmd = db.Database.Connection.CreateCommand();
            var parameters = new SqlParameterSet(cmd);
            StringBuilder categoryFilterSql = new StringBuilder();

            foreach (var category in categories)
            {
                if (categoryFilterSql.Length > 0) categoryFilterSql.AppendLine(" UNION ");
                categoryFilterSql.AppendLine(this.BuildCategoryFilterSql(category, args, parameters));
            }

            // finalize SQL text
            StringBuilder finalSql = new StringBuilder(AppDomain.CurrentDomain
                .GetResourceString("HP.ClearingCenter.Application.ProductGroups.QueryHandlers.Sql.GetReturnObjects.query.sql"));

            finalSql.AppendFormat(" WHERE EXISTS ( SELECT T1.Product_Id FROM ({0}) AS T1 WHERE T1.Product_Id = p.Id GROUP BY T1.Product_Id ) ".WithTokens(categoryFilterSql));

            cmd.CommandText = finalSql.ToString();
            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        private string BuildCategoryFilterSql(ProductGroupCategory category, GetReturnObjectsByProductGroupCodeQuery args, SqlParameterSet parameters)
        {
            StringBuilder productCriteria = new StringBuilder();
            productCriteria.Append(this.BuildCategoryCriteria(category, parameters));
            if (args.ProductId.HasValue) productCriteria.AppendLine(" AND p.Id = {0} ".WithTokens(parameters.Define(args.ProductId)));

            StringBuilder filterSql = new StringBuilder(AppDomain.CurrentDomain
                .GetResourceString("HP.ClearingCenter.Application.ProductGroups.QueryHandlers.Sql.ProductByCategoryCode.template.sql")
                .Replace("[[product_category_criteria]]", productCriteria.ToString()));

            var filterExpressions = new List<string>();

            // left outer join custom attributes
            for (int index = 0; index < category.Filters.Count; index++)
            {
                ProductFilter filter = category.Filters[index];
                filterExpressions.Add(this.BuildCustomAttributeSubQuery(filter, index, filterSql, parameters));
            }   

            filterSql.AppendLine(" WHERE 1 = 1 ");

            // append product id if it exists
            if (args.ProductId.HasValue)
            {
                filterSql.AppendLine(" AND p.Id = {0} ".WithTokens(parameters.Define(args.ProductId.GetValueOrDefault())));
            }

            // append custom attribute filter criteria
            foreach (var expr in filterExpressions)
            {
                filterSql.AppendLine(" AND {0} ".WithTokens(expr));
            }

            return filterSql.ToString();
        }

        private string BuildCustomAttributeSubQuery(ProductFilter filter, int index, StringBuilder filterSql, SqlParameterSet parameters)
        {
            var attr = this.db.CustomAttributes
                .First(x => x.ExternalCode == filter.CustomAttributeExternalCode);

            string customAttributeTable = "TC{0}".WithTokens(index);

            var sql = AppDomain.CurrentDomain
                .GetResourceString("HP.ClearingCenter.Application.ProductGroups.QueryHandlers.Sql.ProductGroupCategoryFilters.template.sql")
                .Replace("[[input_value]]", attr.ValueContainer)
                .Replace("[[custom_attribute_column]]", attr.ExternalCode)
                .Replace("[[custom_attribute_external_code_parameter]]", parameters.Define(filter.CustomAttributeExternalCode))
                .Replace("[[custom_attribute_table]]", customAttributeTable);

            string column = " {0}.{1} ".WithTokens(customAttributeTable, filter.CustomAttributeExternalCode);
            filterSql.AppendLine(sql);

            return this.BuildFilterExpression(filter, attr, column, parameters);
        }

        private string BuildFilterExpression(ProductFilter filter, CustomAttribute attr, string column, SqlParameterSet parameters)
        {
            object[] criteriaValues = filter.GetCriteriaValues(attr.CustomAttributeType);
            bool isMultipleValues = criteriaValues.Length != 0 && criteriaValues.Length > 1;
            string filterTemplate = BuildFilterExpressionTemplate(filter, isMultipleValues);
            string operand = BuildOperand(filter, criteriaValues, isMultipleValues, parameters);
 
            return string.Format(filterTemplate, column, operand);
        }

        private string BuildOperand(ProductFilter filter, object[] criteriaValues, bool isMultipleValues, SqlParameterSet parameters)
        {
            bool isUsingInOperator = isMultipleValues &&
                (filter.FilterOperator == FilterOperator.IsEqualTo || filter.FilterOperator == FilterOperator.IsNotEqualTo);

            if (isUsingInOperator)
            {
                StringBuilder operand = new StringBuilder();

                foreach (var val in criteriaValues)
                {
                    if (operand.Length == 0) operand.Append(", ");
                    operand.Append(parameters.Define(val));
                }

                return " ({0}) ".WithTokens(operand);
            }

            if (isMultipleValues && filter.FilterOperator == FilterOperator.Between)
            {
                var sortedValues = criteriaValues.ToList();
                sortedValues.Sort();
                return " {0} AND {1} ".WithTokens(
                    parameters.Define(sortedValues[0]), 
                    parameters.Define(sortedValues[1]));
            }

            return " {0} ".WithTokens(parameters.Define(criteriaValues[0]));
        }

        private string BuildFilterExpressionTemplate(ProductFilter filter, bool isMultipleValues)
        {
            // define operator
            switch (filter.FilterOperator)
            {
                case FilterOperator.IsEqualTo:
                    return isMultipleValues ? " {0} IN {1} " : " {0} = {1} ";
                case FilterOperator.IsNotEqualTo:
                    return isMultipleValues ? "  {0} NOT IN {1} " : " {0} <> {1} ";
                case FilterOperator.Between:
                    return " {0} BETWEEN {1} ";
                case FilterOperator.GreaterThan:
                    return " {0} > {1} ";
                case FilterOperator.GreaterThanOrEqualTo:
                    return " {0} >= {1} ";
                case FilterOperator.LessThan:
                    return " {0} < {1} ";
                case FilterOperator.LessThanOrEqualTo:
                    return " {0} <= {1} ";
                case FilterOperator.StartsWith:
                    return " {0} LIKE N'{1}%' ";
                case FilterOperator.EndsWith:
                    return " {0} LIKE N'%{1}' ";
                case FilterOperator.SatisfiesExpression:
                    throw new NotImplementedException();
            }

            return " {0} = {1} ";
        }

        private string BuildCategoryCriteria(ProductGroupCategory category, SqlParameterSet parameters)
        {
            StringBuilder result = new StringBuilder();

            foreach (var child in GetChildCategoryCodes(category.CategoryExternalCode, this.db))
            {
                if (result.Length > 0) result.Append(", ");
                result.AppendFormat("{0}", parameters.Define(child));
            }

            if (result.Length == 0) return string.Empty;

            return " AND c.ExternalCode IN ({0}) ".WithTokens(result.ToString());
        }

        public static IEnumerable<string> GetChildCategoryCodes(string externalCode, ClearingCenterDataContext db)
        {
            var results = new Dictionary<string, string>();
            Queue<string> categoryQueue = new Queue<string>();
            categoryQueue.Enqueue(externalCode);

            do
            {
                string categoryExternalCode = categoryQueue.Dequeue();
                var categories = db.Categories
                    .Where(x => 
                        x.ExternalCode == categoryExternalCode ||
                        (x.ParentCategory != null && x.ParentCategory.ExternalCode == categoryExternalCode))
                    .Select(x => x.ExternalCode)
                    .ToList();

                foreach (var catCode in categories)
                {
                    if (!results.ContainsKey(catCode)) results.Add(catCode, catCode);
                    if (catCode != categoryExternalCode) categoryQueue.Enqueue(catCode);
                }

            }
            while (categoryQueue.Any());

            var sortedResults = results.Values.ToList();
            sortedResults.Sort();

            return sortedResults;
        }
    }

    class SqlParameterSet
    {
        private IDbCommand cmd;

        public SqlParameterSet(IDbCommand cmd)
        {
            this.cmd = cmd;
        }

        public string Define(object value)
        {   
            var param = this.cmd.CreateParameter();
            
            param.ParameterName = "p{0}".WithTokens(this.cmd.Parameters.Count);
            param.Value = value;
            this.cmd.Parameters.Add(param);

            return "@" + param.ParameterName;
        }
    }
}
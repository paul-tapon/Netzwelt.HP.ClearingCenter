using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace HP.ClearingCenter.Application.ProductGroups.CommandHandlers
{
    public class AddCategoryAttributeFilterCommandHandler : ICommandHandler<AddCategoryAttributeFilterCommand>
    {
        public void Execute(ICommandContext<AddCategoryAttributeFilterCommand> context)
        {   
            using (var db = new ClearingCenterDataContext())
            {
                // get product group
                var pg = db.ProductGroups.First(x => x.Id == context.Args.ProductGroupId);

                // get category
                var cat = db.Categories.First(x => x.Id == context.Args.CategoryId);

                // get product group category 
                var pgc = db.ProductGroupCategories.First(x => x.Id == context.Args.ProductGroupCategoryId);

                // get custom attribute
                var ca = db.CustomAttributes
                    .Include(x => x.CustomAttributeDataType)
                    .AsNoTracking()
                    .First(x => x.Id == context.Args.CustomAttributeId);

                if (!this.IsValidFilterValues(context.Args, ca))
                {
                    context.ReportError("Filter values are not valid.");
                    return;
                }

                // check if custom attribute is already used as a filter for the category
                if (!IsCategoryAttributeAvailable(pgc, ca, db))
                {
                    context.ReportError("Custom attribute {0} already exists as a filter.".WithTokens(ca.ShortName));
                    return;
                }

                // get filter operator
                var fo = db.ProductFilterOperators.First(x => x.Id == context.Args.FilterOperatorId);

                var filter = db.ProductFilters.Add(new Entities.ProductFilter
                {
                    Id = Guid.NewGuid(),
                    ProductGroupCategory = pgc,
                    ProductFilterOperator = fo,
                    ProductFilterOperatorId = fo.Id,
                    CustomAttributeExternalCode = ca.ExternalCode,
                    CriteriaText = this.BuildCriteriaText(context.Args)
                });

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private bool IsValidFilterValues(AddCategoryAttributeFilterCommand args, CustomAttribute ca)
        {
            var filterOperator = (FilterOperator)args.FilterOperatorId;
            var numericInputs = new List<double>();

            foreach (var val in args.InputValues)
            {
                string inputValue = (val ?? string.Empty).Trim();
                if (inputValue.IsNullOrEmpty()) return false;

                switch (ca.CustomAttributeType)
                {
                    case CustomAttributeType.String:
                        break;
                    case CustomAttributeType.Integer:
                    case CustomAttributeType.Decimal:
                        double numericResult;
                        if (!double.TryParse(val, out numericResult)) return false;
                        numericInputs.Add(numericResult);
                        break;
                    case CustomAttributeType.Boolean:
                        bool boolResult;
                        if (!bool.TryParse(val, out boolResult)) return false;
                        break;
                }
            }

            if (filterOperator == FilterOperator.Between)
            {
                return numericInputs.Count == 2 &&
                    numericInputs[0] < numericInputs[1];
            }

            return true;
        }

        private bool IsCategoryAttributeAvailable(ProductGroupCategory pgc, CustomAttribute ca,  ClearingCenterDataContext db)
        {
            return !db.ProductFilters.Any(x =>
                x.ProductGroupCategory.Id == pgc.Id &&
                x.CustomAttributeExternalCode == ca.ExternalCode);
        }

        private string BuildCriteriaText(AddCategoryAttributeFilterCommand args)
        {
            var result = new StringBuilder();

            foreach (var val in args.InputValues) {
                if (result.Length > 0) result.Append('|');
                result.Append(val);
            }

            return result.ToString();
        }
    }
}

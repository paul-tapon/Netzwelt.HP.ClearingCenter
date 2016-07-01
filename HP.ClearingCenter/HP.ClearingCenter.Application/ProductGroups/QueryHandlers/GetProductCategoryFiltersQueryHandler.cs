using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.QueryHandlers
{
    public class GetProductCategoryFiltersQueryHandler : IQueryHandler<GetProductCategoryFiltersQuery, IEnumerable<CategoryData>>
    {
        public IEnumerable<CategoryData> Retrieve(GetProductCategoryFiltersQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var productGroupCategories = db.ProductGroupCategories
                    .Where(x => x.ProductGroup.Id == query.ProductGroupId)
                    .OrderBy(x => x.CategoryExternalCode)
                    .ToList();

                foreach (var pgc in productGroupCategories)
                {
                    var categories = this.CategoryDataQuery(db.Categories).ToList();
                    var category = categories.FirstOrDefault(x => x.ExternalCode == pgc.CategoryExternalCode);

                    if (category == null) continue;

                    category.ProductGroupCategoryId = pgc.Id;
                    this.LoadFilters(category, categories, query, db);
                    yield return category;
                }
            }
        }

        private IQueryable<CategoryData> CategoryDataQuery(IQueryable<Category> query)
        {
            return query.Select(x => new CategoryData
            {
                Id = x.Id,
                ParentId = x.ParentCategory != null ? x.ParentCategory.Id : new Nullable<int>(),
                ExternalCode = x.ExternalCode,
                ShortName = x.ShortName,
                Description = x.Description
            });
        }

        private void LoadFilters(CategoryData item, IEnumerable<CategoryData> categories, GetProductCategoryFiltersQuery query, ClearingCenterDataContext db)
        {
            // get assigned attributes and their operators
            this.SetupAssignedFilters(item, query, db);
            
            // get unassign attributes and their corresponding operators
            this.SetupAvailableFilters(item, categories, db);
        }

        private void SetupAssignedFilters(CategoryData item, GetProductCategoryFiltersQuery query, ClearingCenterDataContext db)
        {
            var filters = db.ProductFilters
                .Include(x => x.ProductFilterOperator)
                .Where(x =>
                    x.ProductGroupCategory.ProductGroup.Id == query.ProductGroupId &&
                    x.ProductGroupCategory.Id == item.ProductGroupCategoryId)
                .ToList();

            var results = new List<CategoryFilterAttributeData>();
            foreach (var f in filters)
            {
                var attr = db.CustomAttributes.First(x => x.ExternalCode == f.CustomAttributeExternalCode);
                results.Add(new CategoryFilterAttributeData
                {
                    ProductFilterId = f.Id,
                    AttributeShortCode = f.CustomAttributeExternalCode,
                    AttributeShortName = attr.ShortName,
                    FilterOperator = f.FilterOperator,
                    OperatorShortName = f.ProductFilterOperator.ShortName,
                    Values = f.CriteriaText.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries)
                });
            }

            item.Filters = results;
        }

        private void SetupAvailableFilters(CategoryData item, IEnumerable<CategoryData> categories, ClearingCenterDataContext db)
        {
            IEnumerable<string> attributeCodes = this.GetAttributeCodes(item, categories, db);
            var attributes = db.CustomAttributes.Where(x => attributeCodes.Contains(x.ExternalCode));

            var attributeList = new List<CustomAttributeData>();
            foreach (var attr in attributes)
            {
                var filter = new CustomAttributeData
                {
                    Id = attr.Id,
                    ExternalCode = attr.ExternalCode,
                    ShortName = attr.ShortName,             
                };
                
                attributeList.Add(filter);
            }

            item.AvailableFilters = attributeList;

        }

        private IEnumerable<string> GetAttributeCodes(CategoryData item, IEnumerable<CategoryData> categories, ClearingCenterDataContext db)
        {
            List<string> attributeCodes  = new List<string>();
            this.AddCategoryAttributes(item, categories, attributeCodes, db);

            return attributeCodes
                .Where(x => !item.Filters.Any(f => f.AttributeShortCode == x))
                .Distinct()
                .ToList();
        }

        private void AddCategoryAttributes(CategoryData item, IEnumerable<CategoryData> categories, List<string> categoryCodes, ClearingCenterDataContext db)
        {
            categoryCodes.AddRange(db.CategoryAttributeAssignments
                .Where(x => x.Category.Id == item.Id)
                .Select(x => x.CustomAttributeExternalCode)
                .ToList());
            
            if (item.ParentId.HasValue)
            {
                // get parent category attributes, recurrsively
                var parentItem = categories.First(x => x.Id == item.ParentId);
                AddCategoryAttributes(parentItem, categories, categoryCodes, db);
            }
        }
    }
}

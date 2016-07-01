using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.ProductGroups
{
    public class ProductGroupDetailsViewModel
    {
        private IApplicationContext app;
        
        public ProductGroupDetailsViewModel(IApplicationContext app, bool isAddingNewGroup = false)
        {
            this.app = app;
            this.IsAddingNewGroup = isAddingNewGroup;
            this.Categories = Enumerable.Empty<CategoryData>();
            this.AssignedCategories = Enumerable.Empty<CategoryData>();
        }

        public int? ProductGroupId { get; private set; }

        public bool IsAddingNewGroup { get; private set; }

        public bool IsProductGroupFound { get; private set; }

        public AddProductGroupCommand AddProductGroupCommand { get; set; }

        public AddProductGroupFilterCategoryCommand AddProductGroupFilterCategoryCommand { get; set; }

        public IEnumerable<CategoryData> Categories { get; private set; }

        public IEnumerable<CategoryData> AssignedCategories { get; private set; }

        public IEnumerable<CustomAttributeData> AvailableCategoryFilters
        {
            get
            {
                var results = new Dictionary<string, CustomAttributeData>();
                
                var categories = this.AssignedCategories ?? Enumerable.Empty<CategoryData>();
                foreach (var cat in categories)
                {
                    var attrs = cat.AvailableFilters ?? Enumerable.Empty<CustomAttributeData>();
                    foreach (var attr in attrs)
                    {
                        if (!results.ContainsKey(attr.ExternalCode))
                        {
                            results.Add(attr.ExternalCode, attr);
                        }
                    }
                }

                return results.Values
                    .OrderBy(x => x.ShortName)
                    .ToList();
            }
        }

        public ProductGroupDetailsViewModel Initialize(AddProductGroupCommand command)
        {
            this.AddProductGroupCommand = command ?? new AddProductGroupCommand();
            this.IsAddingNewGroup = true;
            return this;
        }

        public ProductGroupDetailsViewModel Initialize(ProductGroupSearchQuery query) 
        {
            query.Term = null;

            ProductGroupData pg = this.app
                .PerformQuery(query)
                .FirstOrDefault();

            if (pg != null)
            {
                this.ProductGroupId = pg.Id;
                this.AddProductGroupCommand = new AddProductGroupCommand
                {
                    ProductGroupId = pg.Id,
                    MarketingProgramId = pg.MarketingProgramId,
                    CountryIsoCode = pg.CountryIsoCode,
                    ExternalCode = pg.ExternalCode,
                    ShortName = pg.ShortName,
                    Description = pg.Description,
                    IsActive = pg.IsActive,
                };

                this.AddProductGroupFilterCategoryCommand = new AddProductGroupFilterCategoryCommand
                {
                    ProductGroupId = pg.Id
                };

                this.InitializeCategoryAttributes(pg);

                this.IsProductGroupFound = true;
            }

            this.Categories = this.app.PerformQuery(new ProductCategorySearchQuery());

            return this;
        }

        private void InitializeCategoryAttributes(ProductGroupData pg)
        {
            this.AssignedCategories = this.app.PerformQuery(new GetProductCategoryFiltersQuery 
            { 
                ProductGroupId = pg.Id
            });
        }
    }
}
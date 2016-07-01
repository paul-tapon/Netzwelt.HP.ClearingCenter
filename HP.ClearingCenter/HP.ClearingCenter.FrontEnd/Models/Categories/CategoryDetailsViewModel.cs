using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models.Categories
{
    public class CategoryDetailsViewModel
    {
        private IApplicationContext app;

        public CategoryDetailsViewModel(IApplicationContext applicationContext)
        {   
            this.app = applicationContext;
            this.AllCategories = Enumerable.Empty<CategoryData>();
            this.AssignedAttributes = Enumerable.Empty<CustomAttributeData>();
            this.AddCategoryAttributeCommand = new AddCategoryAttributeCommand();
            this.Command = new AddOrUpdateCategoryCommand();
        }

        public IEnumerable<CategoryData> AllCategories { get; private set; }

        public IEnumerable<CustomAttributeData> AssignedAttributes { get; private set; }

        public IEnumerable<SelectListItem> AvailableAttributes { get; set; }

        public AddOrUpdateCategoryCommand Command { get; private set; }

        public AddCategoryAttributeCommand AddCategoryAttributeCommand { get; private set; }

        public bool IsCategoryFound { get; private set; }

        public bool IsAddNew { get; private set; }

        public CategoryDetailsViewModel Initialize(int id)
        {
            var cat = this.app
                .PerformQuery(new ProductCategorySearchQuery { CategoryId = id })
                .FirstOrDefault();

            if (cat.Exists())
            {
                this.Command.CategoryId = cat.Id;
                this.Command.ExternalCode = cat.ExternalCode;
                this.Command.ShortName = cat.ShortName;
                this.Command.ParentCategoryId = cat.ParentId;
                this.Command.Description = cat.Description;
                this.AddCategoryAttributeCommand.CategoryId = cat.Id;
                this.IsCategoryFound = true;

                this.InitializeAllCategories();
                this.InitializeAssignedAttributes(cat.Id);
                this.InitializeAvailableAttributes();
            }

            return this;
        }

        private void InitializeAvailableAttributes()
        {
            IEnumerable<string> assignedAttributes = this.AssignedAttributes
                .Select(x => x.ExternalCode);

            this.AvailableAttributes = this.app
                .PerformQuery(new GetAvailableAttributesQuery { AssignedAttributes = assignedAttributes })
                .Select(x => new SelectListItem { Text = x.ShortName, Value = x.ExternalCode });
        }

        public CategoryDetailsViewModel Initialize(AddOrUpdateCategoryCommand command)
        {
            this.Command = command;
            this.IsAddNew = true;
            this.InitializeAllCategories();            
            return this;
        }

        private void InitializeAllCategories()
        {
            this.AllCategories = this.app
                .PerformQuery(new ProductCategorySearchQuery());
        }

        private void InitializeAssignedAttributes(int categoryId)
        {
            this.AssignedAttributes = this.app
                .PerformQuery(new GetCategoryAttributesQuery { CategoryId = categoryId });
        }
    }
}
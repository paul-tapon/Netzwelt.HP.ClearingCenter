using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.CommandHandlers
{
    public class AddOrUpdateCategoryCommandHandler : ICommandHandler<AddOrUpdateCategoryCommand>
    {
        public void Execute(ICommandContext<AddOrUpdateCategoryCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var cat = db.Categories.FirstOrDefault(x => x.Id == context.Args.CategoryId);
                if (cat == null)
                {
                    cat = db.Categories.Add(new Category());
                }

                Category parentCategory = null;
                if (context.Args.ParentCategoryId.GetValueOrDefault() != 0) 
                {
                    parentCategory = db.Categories
                        .Include(x => x.ParentCategory)
                        .First(x => x.Id == context.Args.ParentCategoryId);
                }

                cat.ExternalCode = context.Args.ExternalCode;
                cat.ShortName = context.Args.ShortName;
                cat.ParentCategory = parentCategory;
                cat.Description = context.Args.Description;
                cat.NavigationPath = this.BuildNavigationPath(cat, db);

                db.SaveChanges();
            }
        }

        private string BuildNavigationPath(Category category, ClearingCenterDataContext db)
        {
            // don't build nav path when category is a root node.
            if (category.ParentCategory == null) return null;

            var allCategories = db.Categories
                .Select(x => new CategoryDto {
                    Id = x.Id,
                    ExternalCode = x.ExternalCode,
                    ShortName = x.ShortName,
                    ParentCategoryId = x.ParentCategory != null ? x.ParentCategory.Id : new Nullable<int>() })
                .ToList();

            var cat = allCategories.First(x => x.Id == category.ParentCategory.Id);

            var pathSegments = new List<string>();
            this.AppendParentPath(cat, allCategories, pathSegments);

            return pathSegments.ToDelimitedValues(" » ");
        }

        private void AppendParentPath(CategoryDto cat, IEnumerable<CategoryDto> allCategories, List<string> pathSegments)
        {
            if (cat.ParentCategoryId.HasValue)
            {
                var parent = allCategories.First(x => x.Id == cat.ParentCategoryId);
                this.AppendParentPath(parent, allCategories, pathSegments);
            }

            pathSegments.Add(cat.ShortName);
        }

        class CategoryDto
        {
            public int Id { get; set; }
            public string ExternalCode { get; set; }
            public string ShortName { get; set; }
            public int? ParentCategoryId { get; set; }
        }
    }
}

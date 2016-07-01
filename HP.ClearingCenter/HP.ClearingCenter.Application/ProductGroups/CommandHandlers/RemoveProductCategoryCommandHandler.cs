using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.CommandHandlers
{
    public class RemoveProductCategoryCommandHandler : ICommandHandler<RemoveCategoryCommand>
    {
        public void Execute(ICommandContext<RemoveCategoryCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var cat = db.Categories
                    .FirstOrDefault(x => x.Id == context.Args.CategoryId);

                if (cat == null)
                {
                    context.ReportError("Category with ID '{0}' not found."
                        .WithTokens(context.Args.CategoryId));

                    return;
                }

                var pgc = db.ProductGroupCategories.FirstOrDefault(x =>
                    x.Id == context.Args.ProductGroupCategoryId);

                if (pgc == null)
                {
                    context.ReportError("Category '{0}' is not assigned to product group with id {1}"
                        .WithTokens(cat.ExternalCode, context.Args.ProductGroupId));
                }

                foreach (var filter in db.ProductFilters.Where(x => x.ProductGroupCategory.Id == pgc.Id).ToList())
                {
                    db.ProductFilters.Remove(filter);
                }

                db.ProductGroupCategories.Remove(pgc);

                db.SaveChanges();
            }
        }
    }
}

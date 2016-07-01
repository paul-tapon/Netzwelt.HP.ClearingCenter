using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.CommandHandlers
{
    public class AddProductGroupFilterCategoryCommandHandler : ICommandHandler<AddProductGroupFilterCategoryCommand>
    {
        public void Execute(ICommandContext<AddProductGroupFilterCategoryCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                this.ExecuteCommand(db, context);
            }
        }

        public void ExecuteCommand(ClearingCenterDataContext db, ICommandContext<AddProductGroupFilterCategoryCommand> context)
        {
            ProductGroup pg = this.GetProductGroup(context.Args, db);
            Category cat = db.Categories.First(x => x.Id == context.Args.CategoryId);            

            var pgc = db.ProductGroupCategories.Add(new ProductGroupCategory
            {
                ProductGroup = pg,
                CategoryExternalCode = cat.ExternalCode
            });

            db.SaveChanges();
        }

        public virtual ProductGroup GetProductGroup(AddProductGroupFilterCategoryCommand args, ClearingCenterDataContext db)
        {
            var result = db.ProductGroups.FirstOrDefault(x => x.Id == args.ProductGroupId);

            Protect.AgainstInvalidOperation(result.IsNull(),
                "Product group with id {0} is not found.", args.ProductGroupId);

            return result;
        }
    }
}

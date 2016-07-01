using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.CommandHandlers
{
    public class AddCategoryAttributeCommandHandler : ICommandHandler<AddCategoryAttributeCommand>
    {
        public void Execute(ICommandContext<AddCategoryAttributeCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                if (db.CategoryAttributeAssignments.Any(x => x.CategoryId == context.Args.CategoryId && x.CustomAttributeExternalCode == context.Args.CustomAttributeExternalCode))
                {
                    context.ReportError("Attribute '{0}' is already assigned to the category.".WithTokens(context.Args.CustomAttributeExternalCode));
                    return;
                }
                
                db.CategoryAttributeAssignments.Add(new Entities.CategoryAttributeAssignment
                {
                    Category = db.Categories.First(x => x.Id == context.Args.CategoryId),
                    CustomAttributeExternalCode = context.Args.CustomAttributeExternalCode
                });

                db.SaveChanges();                
            }
        }
    }
}

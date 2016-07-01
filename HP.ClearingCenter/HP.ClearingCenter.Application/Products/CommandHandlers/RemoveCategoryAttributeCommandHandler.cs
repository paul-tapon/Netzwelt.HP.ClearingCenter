using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.CommandHandlers
{
    public class RemoveCategoryAttributeCommandHandler : ICommandHandler<RemoveCategoryAttributeCommand>
    {
        public void Execute(ICommandContext<RemoveCategoryAttributeCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var categoryAttribute = db.CategoryAttributeAssignments
                    .FirstOrDefault(x =>
                        x.CategoryId == context.Args.CategoryId && 
                        x.CustomAttributeExternalCode == x.CustomAttributeExternalCode);

                if (categoryAttribute == null) return;

                db.CategoryAttributeAssignments.Remove(categoryAttribute);
                db.SaveChanges();
            }
        }
    }
}

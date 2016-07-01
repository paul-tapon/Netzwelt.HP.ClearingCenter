using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.CommandHandlers
{
    public class RemoveCategoryAttributeFilterCommandHandler : ICommandHandler<RemoveCategoryAttributeFilterCommand>
    {
        public void Execute(ICommandContext<RemoveCategoryAttributeFilterCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var filter = db.ProductFilters.FirstOrDefault(x => x.Id == context.Args.ProductFilterId);
                if (filter == null) return;

                db.ProductFilters.Remove(filter);
                db.SaveChanges();
            }
        }
    }
}

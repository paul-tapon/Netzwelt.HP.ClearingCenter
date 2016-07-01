using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.CommandHandlers
{
    public class AddOrUpdateManufacturerCommandHandler : ICommandHandler<AddOrUpdateManufacturerCommand>
    {
        public void Execute(ICommandContext<AddOrUpdateManufacturerCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var m = db.Manufacturers.FirstOrDefault(x => x.Id == context.Args.ManufacturerId);
                if (m == null)
                {
                    m = db.Manufacturers.Add(new Manufacturer());
                }

                // ensure external code is unique
                string extCode = (context.Args.ExternalCode ?? string.Empty).Trim();

                if (m.ExternalCode != context.Args.ExternalCode && db.Manufacturers.Any(x => x.ExternalCode == extCode))
                {
                    context.ReportError("External code '{0}' already exists.".WithTokens(context.Args.ExternalCode));
                    return;
                }

                m.Shortname = context.Args.Shortname.Trim();
                m.ExternalCode = context.Args.ExternalCode.Trim();
                m.Description = (context.Args.Description ?? string.Empty).ForceNullIfEmpty();

                db.SaveChanges();
            }
        }
    }
}

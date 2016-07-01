using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.CommandHandlers
{
    public class AddOrUpdateCustomAttributeCommandHandler : ICommandHandler<AddOrUpdateCustomAttributeCommand>
    {
        public void Execute(ICommandContext<AddOrUpdateCustomAttributeCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                int customAttributeDataTypeId = int.Parse(context.Args.CustomAttributeTypeId);
                var dataType = db.CustomAttributeDataTypes.First(x => x.Id == customAttributeDataTypeId);
                
                var ca = db.CustomAttributes.FirstOrDefault(x => x.Id == context.Args.CustomAttributeId);
                if (ca == null)
                {
                    ca = db.CustomAttributes.Add(new CustomAttribute());
                }

                // ensure external code is unique
                string extCode = (context.Args.ExternalCode ?? string.Empty).Trim().Replace(' ', '_').ToLowerInvariant();

                if (ca.ExternalCode != context.Args.ExternalCode && db.CustomAttributes.Any(x => x.ExternalCode == extCode))
                {
                    context.ReportError("External code '{0}' already exists".WithTokens(context.Args.ExternalCode));
                    return;
                }

                ca.ShortName = context.Args.Shortname.Trim();
                ca.ExternalCode = extCode;
                ca.CustomAttributeDataType = dataType;
                ca.UnitText = context.Args.UnitText;
                ca.TranslatorShortcut = context.Args.TranslatorShortcut;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}

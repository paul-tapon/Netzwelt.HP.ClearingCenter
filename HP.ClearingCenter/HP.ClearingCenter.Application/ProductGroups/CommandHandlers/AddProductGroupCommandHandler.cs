using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.CommandHandlers
{
    public class AddProductGroupCommandHandler : ICommandHandler<AddProductGroupCommand>
    {
        const int NULL_PRODUCTGROUP_ID = -99999;

        public void Execute(ICommandContext<AddProductGroupCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                this.ExecuteCommand(db, context);
                db.SaveChanges();
            }
        }

        public void ExecuteCommand(ClearingCenterDataContext db, ICommandContext<AddProductGroupCommand> context)
        {
            var pg = this.GetProductGroup(db, context);

            if (pg != null)
            {
                pg.ExternalCode = context.Args.ExternalCode.ToLowerInvariant();
                pg.ShortName = context.Args.ShortName;
                pg.MarketingProgramId = context.Args.MarketingProgramId.Value;
                pg.CountryIsoCode = context.Args.CountryIsoCode.ToLowerInvariant();
                pg.IsActive = context.Args.IsActive;
                pg.Description = context.Args.Description;
            }
        }

        public virtual ProductGroup GetProductGroup(ClearingCenterDataContext db, ICommandContext<AddProductGroupCommand> context)
        {
            var args = context.Args;
            var productGroupId = args.ProductGroupId.GetValueOrDefault(NULL_PRODUCTGROUP_ID);
            
            var result = db.ProductGroups.FirstOrDefault(x => x.Id == productGroupId);

            if (result == null)
            {
                var existingProductGroup = db.ProductGroups.FirstOrDefault(x =>
                    x.MarketingProgramId == args.MarketingProgramId &&
                    x.CountryIsoCode == args.CountryIsoCode &&
                    x.ExternalCode == args.ExternalCode);

                if (existingProductGroup != null)
                {
                    context.ReportError("Product group '{0}' already exists for program {1} - {2}".WithTokens(args.ExternalCode, args.MarketingProgramId, args.CountryIsoCode));
                    return null;
                }

                result = db.ProductGroups.Add(new ProductGroup());
            }

            return result;
        }
    }
}

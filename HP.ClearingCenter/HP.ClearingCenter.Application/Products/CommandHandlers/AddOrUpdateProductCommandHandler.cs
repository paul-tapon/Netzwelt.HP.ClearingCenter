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
    public class AddOrUpdateProductCommandHandler : ICommandHandler<AddOrUpdateProductCommand>
    {
        private IApplicationContext app;

        public AddOrUpdateProductCommandHandler(IApplicationContext app)
        {
            this.app = app;
        }

        public void Execute(ICommandContext<AddOrUpdateProductCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var pd = db.Products.FirstOrDefault(x => x.Id == context.Args.ProductId);
                if (pd == null)
                {
                    pd = db.Products.Add(new Product { CreatedAt = this.app.GetUtcNow(), CreatedBy = this.app.GetCurrentUser().Username });
                }
                else
                {
                    pd.UpdatedAt = this.app.GetUtcNow();
                    pd.UpdatedBy = this.app.GetCurrentUser().Username;
                }

                if (!context.Args.ProductNumber.IsNullOrEmpty() && context.Args.ProductNumber != pd.ProductNumber)
                {
                    if (db.Products.Any(x => x.ProductNumber == context.Args.ProductNumber))
                    {
                        context.ReportError("Product number '{0}' already exists.".WithTokens(context.Args.ProductNumber));
                        return;
                    }
                }

                pd.Category = db.Categories.First(x => x.Id == context.Args.CategoryId);
                pd.Manufacturer = db.Manufacturers.First(x => x.Id == context.Args.ManufacturerId);
                this.MapInputs(pd, context.Args);                

                db.SaveChanges();

                context.SetResponse(pd.Id);
            }
        }

        private void MapInputs(Product pd, AddOrUpdateProductCommand args)
        {
            pd.ProductNumber = args.ProductNumber;
            pd.ShortName = args.ShortName;
            pd.Description = args.Description;            
            if (!args.OriginCountryIsoCode.IsNullOrEmpty()) pd.OriginCountryIsoCode = args.OriginCountryIsoCode;

            if (args.YearOfConstruction.GetValueOrDefault() != default(int))
            {
                pd.YearOfConstruction = args.YearOfConstruction;
            }
            else
            {
                pd.YearOfConstruction = null;
            }

            pd.Length = args.Length;
            pd.Width = args.Width;
            pd.Height = args.Height;
            if (!args.LengthUnit.IsNullOrEmpty()) pd.LengthUnit = args.LengthUnit;
            pd.Weight = args.Weight;
            if (!args.WeightUnit.IsNullOrEmpty()) pd.WeightUnit = args.WeightUnit;
            pd.IsActive = args.IsActive;
        }
    }
}

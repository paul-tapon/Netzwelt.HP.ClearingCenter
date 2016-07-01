using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.CommandHandlers
{
    public class SubmitProductAttributesCommandHandler : ICommandHandler<SubmitProductAttributesCommand>
    {
        public void Execute(ICommandContext<SubmitProductAttributesCommand> context)
        {
            if (context.Args.Values == null || context.Args.Values.Count == 0) return;
            
            using (var db = new ClearingCenterDataContext())
            {
                var product = db.Products.First(x => x.Id == context.Args.ProductId);
                var attributeList = context.Args.Values
                    .Select(x => x.CustomAttributeExternalCode)
                    .Distinct()
                    .ToList();

                var existingValues = db.ProductCustomAttributeValues
                    .Where(x =>
                        x.Product.Id == context.Args.ProductId &&
                        attributeList.Contains(x.CustomAttributeExternalCode))
                    .Select(x => new { x.CustomAttributeExternalCode, AttributeValue = x })
                    .ToDictionary(x => x.CustomAttributeExternalCode, x => x.AttributeValue);

                foreach (var val in context.Args.Values)
                {
                    if (existingValues.ContainsKey(val.CustomAttributeExternalCode)) 
                    {                    
                        db.ProductCustomAttributeValues.Remove(existingValues[val.CustomAttributeExternalCode]);
                    }
                    
                    var attrVal = db.ProductCustomAttributeValues.Add(new ProductCustomAttributeValue
                    {
                        Id = Guid.NewGuid(),
                        CustomAttributeExternalCode = val.CustomAttributeExternalCode,
                        Product = product,
                        StringValue = val.StringValue,
                        BooleanValue = val.BooleanValue,
                        IntegerValue = val.IntegerValue,
                        DecimalValue = val.DecimalValue
                    });
                }

                db.SaveChanges();
            }
        }
    }
}

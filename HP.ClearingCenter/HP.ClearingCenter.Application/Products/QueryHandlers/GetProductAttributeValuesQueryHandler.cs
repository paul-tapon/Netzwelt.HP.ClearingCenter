using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class GetProductAttributeValuesQueryHandler : IQueryHandler<GetProductAttributeValuesQuery, IEnumerable<ProductCustomAttributeValueData>>
    {
        public IEnumerable<ProductCustomAttributeValueData> Retrieve(GetProductAttributeValuesQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var results = db.ProductCustomAttributeValues
                    .Where(x => x.Product.Id == query.ProductId && query.CustomAttributeExternalCodes.Contains(x.CustomAttributeExternalCode))
                    .Select(x => new ProductCustomAttributeValueData {
                        Id = x.Id,
                        CustomAttributeExternalCode = x.CustomAttributeExternalCode,
                        StringValue = x.StringValue,
                        BooleanValue = x.BooleanValue,
                        IntegerValue = x.IntegerValue,
                        DecimalValue = x.DecimalValue })
                    .ToList();

                return results;
            }
        }
    }
}

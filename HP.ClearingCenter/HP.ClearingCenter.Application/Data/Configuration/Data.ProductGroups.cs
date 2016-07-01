using HP.ClearingCenter.Application.ProductGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    public static partial class Data
    {
        public static void ProductGroups(ClearingCenterDataContext db)
        {
            // criteria operators
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.IsEqualTo));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.IsNotEqualTo));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.StartsWith));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.SatisfiesExpression));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.Between));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.GreaterThan));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.GreaterThanOrEqualTo));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.LessThan));
            db.ProductFilterOperators.Add(new ProductFilterOperator(FilterOperator.LessThanOrEqualTo));

            // product groups
            db.ProductGroups.Add(new ProductGroup { 
                ExternalCode = "all_hp_products", 
                IsActive = true, 
                MarketingProgramId = 91000, 
                CountryIsoCode = "de", 
                ShortName = "All HP Products", 
                Description = "All Qualified HP Products" 
            }); 
        }
    }
}

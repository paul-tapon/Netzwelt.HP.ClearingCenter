using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using HP.ClearingCenter.Application.Products.Entities;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class SimpleSearchProductQueryHandler : IQueryHandler<SimpleSearchProductsQuery, IEnumerable<ProductData>>
    {
        public IEnumerable<ProductData> Retrieve(SimpleSearchProductsQuery query)
        {
            using (var db = new ClearingCenterDataContext()) {
                IQueryable<Product> resultSet = db.Products;

                if (query.ProductId.HasValue)
                {
                    resultSet = resultSet.Where(x => x.Id == query.ProductId);
                }
                else
                {
                    string term = (query.Term ?? string.Empty).Trim().ToUpperInvariant();
                    if (!term.IsNullOrEmpty())
                    {
                        resultSet = resultSet.Where(x =>
                            x.Manufacturer.Shortname.ToUpper().StartsWith(term) ||
                            x.ProductNumber.ToUpper().StartsWith(term) ||
                            x.ShortName.ToUpper().Contains(term));
                    }
                }

                var qry = resultSet
                    .OrderBy(x => x.Manufacturer.Shortname)
                    .ThenBy(x => x.ShortName)
                    .Select(x => new ProductData { 
                        ProductId = x.Id, 
                        ManufacturerId = x.Manufacturer.Id,
                        Manufacturer = x.Manufacturer.Shortname, 
                        ProductName = x.ShortName, 
                        ProductNumber = x.ProductNumber, 
                        CategoryId = x.Category.Id,
                        Category = x.Category.ShortName,
                        Description = x.Description,
                        Length = x.Length, 
                        Width = x.Width, 
                        Height = x.Height, 
                        LengthUnit = x.LengthUnit,
                        Weight = x.Weight,
                        WeightUnit = x.WeightUnit,
                        YearOfConstruction = x.YearOfConstruction, 
                        OriginCountryIsoCode = x.OriginCountryIsoCode, 
                        IsActive = x.IsActive });

                return qry.ToPagedList(query.Page, query.MaxItemCount);
            }
        }
    }
}

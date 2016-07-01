using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;

namespace HP.ClearingCenter.Application.ProductGroups.QueryHandlers
{
    public class ProductGroupSearchQueryHandler : IQueryHandler<ProductGroupSearchQuery, IEnumerable<ProductGroupData>>
    {
        public IEnumerable<ProductGroupData> Retrieve(ProductGroupSearchQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                IQueryable<ProductGroup> results = db.ProductGroups;

                if (query.Id.HasValue)
                {
                    results = results.Where(x => x.Id == query.Id);
                }

                if (!query.Term.IsNullOrEmpty())
                {
                    var term = query.Term.Trim().ToUpperInvariant();
                    results = results.Where(x => x.ExternalCode.ToUpper().StartsWith(term));
                }

                return results
                    .OrderBy(x=>x.ShortName)
                    .Select(x => new ProductGroupData
                    {
                        Id = x.Id,
                        MarketingProgramId = x.MarketingProgramId,
                        CountryIsoCode = x.CountryIsoCode,
                        ExternalCode = x.ExternalCode,
                        ShortName = x.ShortName,
                        TranslatorShortcut = x.TranslatorShortcut,
                        Description = x.Description,
                        IsActive = x.IsActive
                    })
                    .ToPagedList(query.Page, query.MaxRowCount);
            }
        }
    }
}

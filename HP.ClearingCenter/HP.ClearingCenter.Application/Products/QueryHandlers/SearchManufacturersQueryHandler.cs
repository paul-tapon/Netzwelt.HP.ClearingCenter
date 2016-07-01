using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class SearchManufacturersQueryHandler : IQueryHandler<SearchManufacturersQuery, IEnumerable<ManufacturerData>>
    {
        public IEnumerable<ManufacturerData> Retrieve(SearchManufacturersQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                //TODO: Implement pagination
                IQueryable<Manufacturer> rs = db.Manufacturers;

                if (query.ManufacturerId.HasValue)
                {
                    return rs
                        .Where(x => x.Id == query.ManufacturerId)
                        .Select(x => new ManufacturerData {
                            Id = x.Id,
                            Shortname = x.Shortname,
                            ExternalCode = x.ExternalCode, 
                            Description = x.Description })
                        .ToList();
                }

                string term = (query.Term ?? string.Empty).Trim();
                if (term.Length > 0)
                {
                    rs = rs.Where(x => x.ExternalCode.StartsWith(term) || x.Shortname.StartsWith(term));
                }

                return rs
                    .OrderBy(x => x.ExternalCode)
                    .Select(x => new ManufacturerData
                    {
                        Id = x.Id,
                        Shortname = x.Shortname,
                        ExternalCode = x.ExternalCode,
                        Description = x.Description
                    })
                    .ToPagedList(query.Page, query.MaxRows);
            }
        }
    }
}

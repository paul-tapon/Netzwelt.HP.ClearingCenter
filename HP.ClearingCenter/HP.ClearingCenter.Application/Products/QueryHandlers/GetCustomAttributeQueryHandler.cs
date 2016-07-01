using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using PagedList;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class GetCustomAttributeQueryHandler : IQueryHandler<GetCustomAttributeQuery, IEnumerable<CustomAttributeData>>
    {
        public IEnumerable<CustomAttributeData> Retrieve(GetCustomAttributeQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                IQueryable<CustomAttribute> resultSet = db.CustomAttributes
                    .Include(x => x.OptionListItems);

                if (query.CustomAttributeId.HasValue)
                {
                    resultSet = resultSet.Where(x => x.Id == query.CustomAttributeId);
                }
                else
                {
                    string term = (query.ExternalCode ?? string.Empty).Trim().ToUpperInvariant();
                    if (term.Length > 0)
                    {
                        resultSet = resultSet.Where(x => 
                            x.ExternalCode.ToUpper().StartsWith(term) || 
                            x.ShortName.ToUpper().StartsWith(term));
                    }
                }

                //TODO: implement pagination

                var pagedResult = resultSet
                    .Include(x => x.CustomAttributeDataType)
                    .OrderBy(x => x.ShortName)
                    .ToPagedList(query.Page, query.MaxRowCount);


                var customerAttributes =
                    pagedResult.Select(x => new CustomAttributeData
                    {
                        Id = x.Id,
                        ExternalCode = x.ExternalCode,
                        ShortName = x.ShortName,
                        UnitText = x.UnitText,
                        TranslatorShortcut = x.TranslatorShortcut,
                        IsOptionListItemsEnabled = x.IsOptionListItemsEnabled,
                        IsStrictToOptions = x.IsStrictToOptions,
                        CustomAttributeType = x.CustomAttributeType,
                        Operators = x.ValidOperators.ToArray(),
                        OptionListItems = Utils.GetOptionListItems(x)
                    });


                return new StaticPagedList<CustomAttributeData>(customerAttributes, query.Page, query.MaxRowCount, pagedResult.TotalItemCount);

            }
        }


    }
}

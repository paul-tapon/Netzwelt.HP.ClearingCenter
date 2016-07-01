using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class GetAvailableAttributesQueryHandler : IQueryHandler<GetAvailableAttributesQuery, IEnumerable<CustomAttributeData>>
    {
        public IEnumerable<CustomAttributeData> Retrieve(GetAvailableAttributesQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                return db.CustomAttributes
                    .Where(x => !query.AssignedAttributes.Contains(x.ExternalCode))
                    .OrderBy(x => x.ShortName)
                    .Select(x => new CustomAttributeData
                    {
                        Id = x.Id,
                        ExternalCode = x.ExternalCode,
                        ShortName = x.ShortName,
                        UnitText = x.UnitText,
                        TranslatorShortcut = x.TranslatorShortcut
                    })
                    .ToList();
            }
        }
    }
}

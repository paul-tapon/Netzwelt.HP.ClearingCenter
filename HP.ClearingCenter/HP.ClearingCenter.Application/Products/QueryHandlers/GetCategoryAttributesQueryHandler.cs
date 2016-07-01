using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ClearingCenter.Application.Data.Entities;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class GetCategoryAttributesQueryHandler : IQueryHandler<GetCategoryAttributesQuery, IEnumerable<CustomAttributeData>>
    {
        public IEnumerable<CustomAttributeData> Retrieve(GetCategoryAttributesQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                return PerformQuery(query, db);
            }
        }

        public static IEnumerable<CustomAttributeData> PerformQuery(GetCategoryAttributesQuery query, ClearingCenterDataContext db)
        {
            IEnumerable<AttributeDto> customAttributeShortCodes = GetCustomAttributeShortcodes(query, db);
            IEnumerable<CustomAttribute> resultSet = FilterCustomAttributes(customAttributeShortCodes.Select(x => x.ExternalCode), db);

            return from attr in resultSet
                   join code in customAttributeShortCodes on attr.ExternalCode equals code.ExternalCode
                   select new CustomAttributeData
                   {
                       Id = attr.Id,
                       ExternalCode = attr.ExternalCode,
                       ShortName = attr.ShortName,
                       UnitText = attr.UnitText,
                       TranslatorShortcut = attr.TranslatorShortcut,
                       IsStrictToOptions = attr.IsStrictToOptions,
                       IsOptionListItemsEnabled = attr.IsOptionListItemsEnabled,
                       CustomAttributeType = (CustomAttributeType)attr.CustomAttributeDataType.Id,
                       OptionListItems = Utils.GetOptionListItems(attr),
                       IsInherited = code.IsInherited
                   };
        }

        private static IEnumerable<CustomAttribute> FilterCustomAttributes(IEnumerable<string> shortCodes, ClearingCenterDataContext db)
        {
            return db.CustomAttributes
                .Include(x => x.OptionListItems)
                .Include(x => x.CustomAttributeDataType)
                .Where(x => shortCodes.Contains(x.ExternalCode))
                .OrderBy(x => x.ShortName)
                .ToList();
        }

        private static IEnumerable<AttributeDto> GetCustomAttributeShortcodes(GetCategoryAttributesQuery args, ClearingCenterDataContext db)
        {
            var results = new Dictionary<string, AttributeDto>();

            Category cat = null;
            int categoryId = args.CategoryId;
            bool isInherited = false;

            do
            {
                cat = db.Categories
                    .Include(x => x.ParentCategory)
                    .First(x => x.Id == categoryId);

                var attributes = db.CategoryAttributeAssignments
                      .Where(x => x.Category.Id == categoryId)
                      .Select(x => new AttributeDto { ExternalCode = x.CustomAttributeExternalCode, IsInherited = isInherited });

                foreach (var attr in attributes)
                {
                    if (!results.ContainsKey(attr.ExternalCode))
                    {
                        results.Add(attr.ExternalCode, attr);
                    }
                }

                if (cat.ParentCategory != null)
                {
                    categoryId = cat.ParentCategory.Id;
                    isInherited = true;
                }
            }
            while (cat.ParentCategory != null);

            return results.Values;
        }

        class AttributeDto
        {
            public string ExternalCode { get; set; }
            public bool IsInherited { get; set; }
        }
    }
}

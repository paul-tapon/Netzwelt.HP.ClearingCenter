using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public static class Utils
    {
        public static KeyValueData[] GetOptionListItems(CustomAttribute attr)
        {
            if (attr.OptionListItems == null) return new KeyValueData[0];

            return attr.OptionListItems
                .Select(x => new KeyValueData
                {
                    DisplayText = x.DisplayText,
                    ValueText = x.ValueText,
                    TranslatorShortcut = x.TranslatorShortcut,
                })
                .ToArray();
        }
    }
}

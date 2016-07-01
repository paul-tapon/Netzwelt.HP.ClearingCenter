using HP.ClearingCenter.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class CustomAttributeData
    {
        public int Id { get; set; }
        public string ExternalCode { get; set; }
        public string ShortName { get; set; }
        public string UnitText { get; set; }
        public string TranslatorShortcut { get; set; }
        public bool IsOptionListItemsEnabled { get; set; }
        public bool IsStrictToOptions { get; set; }
        public bool? IsInherited { get; set; }
        public CustomAttributeType CustomAttributeType { get; set; }
        public FilterOperatorData[] Operators { get; set; }
        public KeyValueData[] OptionListItems { get; set; }
    }
}

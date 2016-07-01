using HP.ClearingCenter.Application.ProductGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class CategoryData
    {
        public CategoryData()
        {
            this.Filters = Enumerable.Empty<CategoryFilterAttributeData>();
            this.AvailableFilters = Enumerable.Empty<CustomAttributeData>();
        }

        public int? ProductGroupCategoryId { get; set; }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string ParentCategoryName { get; set; }

        public string ExternalCode { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public string NavigationPath { get; set; }

        public IEnumerable<CategoryFilterAttributeData> Filters { get; set; }

        public IEnumerable<CustomAttributeData> AvailableFilters { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class ProductGroupCategoryData
    {
        public int ProductGroupId { get; set; }
        public string CategoryExternalCode { get; set; }
        public int ProductGroupCategoryId { get; set; }
        public CategoryData CategoryData { get; set; }
    }
}

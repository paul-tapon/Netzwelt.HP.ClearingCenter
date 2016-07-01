using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class ReturnObjectData
    {
        public int Id { get; set; }

        public int ReturnObjectTypeId { get; set; }

        public int? CC_CategoryId { get; set; }

        public int? CC_TechnologyId { get; set; }

        public int CC_ManufacturerId { get; set; }

        public int CC_ProductId { get; set; }

        public string CC_Description { get; set; }

        public string CC_ManufacturerDescription { get; set; }
    }
}

using HP.ClearingCenter.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class ProductCustomAttributeValueData
    {
        public Guid? Id { get; set; }

        public string CustomAttributeExternalCode { get; set; }

        [StringLength(1024)]
        public string StringValue { get; set; }

        public bool? BooleanValue { get; set; }

        public int? IntegerValue { get; set; }

        public decimal? DecimalValue { get; set; }
    }
}

using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Entities
{
    [Table("T_ProductCustomAttributeValue", Schema=Schemas.Products)]
    public class ProductCustomAttributeValue
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomAttributeExternalCode { get; set; }

        [StringLength(1024)]
        public string StringValue { get; set; }

        public bool? BooleanValue { get; set; }

        public int? IntegerValue { get; set; }

        public decimal? DecimalValue { get; set; }


    }
}

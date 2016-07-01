using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Entities
{
    [Table("D_ProductFilterOperator", Schema = Schemas.ProductGroups)]
    public class ProductFilterOperator
    {
        public ProductFilterOperator() {}

        public ProductFilterOperator(FilterOperator filterOperator)
        {
            this.Id = (int)filterOperator;
            this.ShortName = filterOperator.ToString();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(256)]
        public string TranslatorShortcut { get; set; }
    }
}

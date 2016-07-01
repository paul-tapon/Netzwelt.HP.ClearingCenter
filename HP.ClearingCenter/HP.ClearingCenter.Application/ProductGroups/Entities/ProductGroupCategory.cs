using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Entities
{
    [Table("T_ProductGroupCategory", Schema = Schemas.ProductGroups)]
    public class ProductGroupCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ProductGroup ProductGroup { get; set; }

        [Required]
        [StringLength(128)]
        public string CategoryExternalCode { get; set; }

        [InverseProperty("ProductGroupCategory")]
        public virtual IList<ProductFilter> Filters { get; set; }
    }
}

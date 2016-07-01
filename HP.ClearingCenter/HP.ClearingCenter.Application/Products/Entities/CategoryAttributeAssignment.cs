using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Entities
{
    [Table("T_CategoryAttributeAssignment", Schema = Schemas.Products)]
    public class CategoryAttributeAssignment
    {
        [Key]
        [Column(Order = 1)]
        public int CategoryId { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        [StringLength(128)]
        public string CustomAttributeExternalCode { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("AttributeAssignments")]
        public Category Category { get; set; }
    }
}

using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Entities
{
    [Table("T_Category", Schema = Schemas.Products)]
    public class Category
    {
        public Category()
        {
            this.ChildCategories = new List<Category>();
            this.AttributeAssignments = new List<CategoryAttributeAssignment>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        public Category ParentCategory { get; set; }

        [StringLength(1024)]
        public string NavigationPath { get; set; }

        [InverseProperty("ParentCategory")]
        public virtual IList<Category> ChildCategories { get; private set; }

        public virtual IList<CategoryAttributeAssignment> AttributeAssignments { get; private set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string TranslatorShortcut { get; set; }
    }
}

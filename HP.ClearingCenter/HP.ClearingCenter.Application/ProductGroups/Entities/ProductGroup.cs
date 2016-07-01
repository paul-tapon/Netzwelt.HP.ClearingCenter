using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Entities
{
    [Table("T_ProductGroup", Schema = Schemas.ProductGroups)]
    public class ProductGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MarketingProgramId { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryIsoCode { get; set; }

        [Required]
        [StringLength(128)]
        [Index("IX_ProductGroup_ExternalCode", Order = 1, IsUnique = true)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(256)]
        public string TranslatorShortcut { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}

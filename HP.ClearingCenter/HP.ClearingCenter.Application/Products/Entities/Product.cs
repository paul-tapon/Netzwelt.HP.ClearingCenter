using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Entities
{
    [Table("T_Product", Schema = Schemas.Products)]
    public class Product
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
                
        [StringLength(128)]
        public string ProductNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
                
        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        [StringLength(128)]
        public string LengthUnit { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(128)]
        public string WeightUnit { get; set; }

        public int? YearOfConstruction { get; set; }

        [StringLength(3)]
        public string OriginCountryIsoCode { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }
                
        public DateTime CreatedAt { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
                
        public string Remarks { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public Manufacturer Manufacturer { get; set; }

        public Category Category { get; set; }

        public int? ExternalProductId { get; set; }
    }
}

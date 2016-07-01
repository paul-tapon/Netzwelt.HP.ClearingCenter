using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class Product
    {
        public int? ProductId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Manufacturer { get; set; }

        [Required]
        [MaxLength(128)]
        public string Model { get; set; }

        [MaxLength(128)]
        public string PartNumber { get; set; }

        public int? ManufacturingYear { get; set; }

        [MaxLength(3)]
        public string CountryOfOrigin { get; set; }

        [MaxLength(128)]
        public string SerialNumber { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public int? Length { get; set; }

        [MaxLength(128)]
        public string LengthUnit { get; set; }

        public int? Weight { get; set; }

        [MaxLength(128)]
        public string WeightUnit { get; set; }
    }
}

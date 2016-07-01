using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Entities
{
    [Table("T_Country")]
    public class Country
    {
        [Key]
        [Required]
        [StringLength(3)]
        public string IsoCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Legalname { get; set; }

        [StringLength(128)]        
        public string DisplayName { get; set; }

        [Required]
        [StringLength(3)]
        public string DefaultLanguageIsoCode { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrencyIsoCode { get; set; }

        [Required]
        [StringLength(256)]
        public string DefaultTimeZoneCode { get; set; }
    }
}

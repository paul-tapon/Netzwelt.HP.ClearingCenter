using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Entities
{
    [Table("T_Translator")]
    public class Translator
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryIsoCode { get; set; }

        [Required]
        [StringLength(3)]
        public string LanguageIsoCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Shortcut { get; set; }

        [Required]
        public string TextValue { get; set; }

        [StringLength(512)]
        public string Remarks { get; set; }
    }
}

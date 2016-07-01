using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Entities
{
    [Table("T_OptionListItem")]
    public class OptionListItem
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]        
        [InverseProperty("OptionListItems")]
        public CustomAttribute CustomAttribute { get; set; }

        [Required]
        [StringLength(128)]
        public string ValueText { get; set; }

        [StringLength(128)]
        public string DisplayText { get; set; }

        [StringLength(256)]
        public string TranslatorShortcut { get; set; }
    }
}

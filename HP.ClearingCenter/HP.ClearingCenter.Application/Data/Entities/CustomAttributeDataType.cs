using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Entities
{
    [Table("D_CustomAttributeDataType")]
    public class CustomAttributeDataType
    {
        public CustomAttributeDataType() { }

        public CustomAttributeDataType(CustomAttributeType customAttributeType)
        {
            this.Id = (int)customAttributeType;
            this.ShortName = customAttributeType.ToString();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set;}

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(256)]
        public string TranslatorShortcut { get; set; }

        [StringLength(512)]
        public string Description { get; set; }
    }
}

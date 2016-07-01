using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    [Table("D_MarketingProgramType", Schema = Schemas.TransactionTransports)]
    public class MarketingProgramType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(128)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(128)]
        public string TranslatorShortcut { get; set; }
    }
}

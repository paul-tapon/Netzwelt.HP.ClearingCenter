using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    [Table("T_ForwardingInstruction", Schema = Schemas.TransactionTransports)]
    public class ForwardingInstruction : IForwardingInstruction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(128)]
        public string ExternalCode { get; set; }

        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(128)]
        public string TranslatorShortcut { get; set; }
    }
}

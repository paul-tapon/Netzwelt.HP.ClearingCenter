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
    [Table("T_StatusCode", Schema = Schemas.TransactionTransports)]
    public class StatusCode : IStatusCode
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        public bool IsError { get; set; }

        public bool IsReceiving { get; set; }

        public bool IsClearing { get; set; }

        [StringLength(128)]
        public string TranslatorShortcut { get; set; }
    }
}

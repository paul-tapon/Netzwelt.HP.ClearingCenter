using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    [Table("T_Transaction", Schema = Schemas.TransactionTransports)]
    public class TransactionHeader
    {
        public TransactionHeader()
        {
            this.Details = new List<TransactionDetail>();
        }

        [Key]
        [StringLength(128)]
        public string QuoteNumber { get; set; }

        [StringLength(128)]
        public string OfferId { get; set; }

        public LocalProgram LocalProgram { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [StringLength(128)]
        public string ContactCompany { get; set; }

        [StringLength(128)]
        public string ContactCustomer { get; set; }

        public Address ContactAddress { get; set; }

        public virtual IList<TransactionDetail> Details { get; private set; }
    }
}

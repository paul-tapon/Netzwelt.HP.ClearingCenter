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
    [Table("T_TransactionDetail", Schema = Schemas.TransactionTransports)]
    public class TransactionDetail
    {
        public TransactionDetail()
        {
            this.PickupAddress = new Address();
        }

        [Key]
        [StringLength(128)]
        public string TransportNumber { get; set; }

        [StringLength(256)]
        public string ExternalId { get; set; }

        [Required]
        [ForeignKey("TransactionHeader")]
        public string QuoteNumber { get; set; }

        public TransactionHeader TransactionHeader { get; set; }

        #region product group

        public int ProductGroupId { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductGroupExternalCode { get; set; }

        [StringLength(512)]
        public string ProductGroupDescription { get; set; }

        #endregion

        [StringLength(128)]
        public string SerialNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? EstimatedArrivalDate { get; set; }

        [StringLength(128)]
        public string PickupCompany { get; set; }

        [StringLength(128)]
        public string PickupCustomer { get; set; }

        public Address PickupAddress { get; set; }

        public int UnitsAdvised { get; set; }

        [Required]
        [StringLength(128)]
        public string ManufacturerNameAdvised { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductNameAdvised { get; set; }
                
        [StringLength(128)]
        public string ProductNumberAdvised { get; set; }

        #region receiving

        public int? UnitsReceived { get; set; }

        public DateTime? ReceivingDate { get; set; }

        public DateTime? ReceivingConfirmationDate { get; set; }

        [StringLength(128)]
        public string ReceivingConfirmedBy { get; set; }

        [StringLength(128)]
        public string ReceivedBy { get; set; }

        [StringLength(512)]
        public string ReceivingRemarks { get; set; }

        public StatusCode ReceivingStatus { get; set; }

        #endregion

        #region clearing
        
        [StringLength(128)]
        public string ManufacturerNameReceived { get; set; }

        [StringLength(128)]
        public string ProductNameReceived { get; set; }

        [StringLength(128)]
        public string ProductNumberReceived { get; set; }

        public int? ProductId { get; set; }
        
        public DateTime? ClearingDate { get; set; }

        public DateTime? ClearingConfirmationDate { get; set; }

        [StringLength(128)]
        public string ClearingConfirmedBy { get; set; }

        [StringLength(128)]
        public string ClearedBy { get; set; }

        [StringLength(512)]
        public string ClearingRemarks { get; set; }

        public StatusCode ClearingStatus { get; set; }

        public ForwardingInstruction ForwardingInstruction { get; set; }

        #endregion
    }
}

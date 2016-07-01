using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class OrderPosition
    {
        [Required]
        [MaxLength(128)]
        public string TransportNumber { get; set; }

        public int ProductGroupId { get; set; }

        [MaxLength(128)]
        public string ClearingCenterId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        [MaxLength(128)]
        public string TransactionDetailId { get; set; }
        
        public Product Product { get; set; }

        public ContactAddress PickupAddress { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime? ExpectedArrivalDate { get; set; }

        public CustomAttribute[] ClearingProcessAttributes { get; set; }

    }
}

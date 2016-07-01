using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class Order
    {
        [Required]
        public string MarketingProgramId { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryIsoCode { get; set; }

        [Required]
        [MaxLength(128)]
        public string OfferId { get; set; }

        [Required]
        [MaxLength(128)]
        public string QuoteNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public ContactAddress CustomerData { get; set; }

        public OrderPosition[] Positions { get; set; }
    }
}

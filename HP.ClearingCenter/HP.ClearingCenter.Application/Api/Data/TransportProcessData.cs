using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class TransportProcessData
    {
        public int MarketingProgramId { get; set; }
        public string CountryIsoCode { get; set; }
        public string OfferId { get; set; }
        public string QuoteNumber { get; set; }
        public string TransportNumber { get; set; }

        public string ManufacturerName { get; set; }
        public string ProductNumber {get; set;}
        public string ProductName { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        
        public string ForwardingInstruction { get; set; }

        public StatusData ReceivingStatus { get; set; }
        public bool IsReceivingOk { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public string ReceivedBy { get; set;}
        public string ReceivingRemarks { get; set; }
        public string ProblemCodeReceiving { get; set; }

        public StatusData ClearingStatus { get; set; }
        public bool IsClearingOk { get; set; }
        public DateTime? ClearingDate { get; set; }
        public string ClearedBy { get; set; }
        public string ClearingRemarks { get; set; }
        public string ProblemCodeClearing { get; set; }

        public ProductGroupData ProductGroup { get; set; }
    }
}

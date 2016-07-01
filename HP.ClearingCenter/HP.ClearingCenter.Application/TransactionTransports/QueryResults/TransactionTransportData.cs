using HP.ClearingCenter.Application.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryResults
{
    public class TransactionTransportData
    {
        public string ProcessType { get; set; }

        public int ProgramNumber { get; set; }

        public string ProgramName { get; set; }

        public string ProgramType { get; set; }
        
        public string QuoteNumber { get; set; }

        public string TransportNumber { get; set; }

        public ProductData ProductAdvised { get; set; }

        public int UnitsAdvised { get; set; }

        public DateTime PickupDate { get; set; }
    
        #region receiving context

        public IStatusCode ReceivingStatus { get; set; }

        public DateTime? ReceivingDate { get; set; }

        public string ReceivedBy { get; set; }

        public string ReceivingRemarks { get; set; }

        public int? UnitsReceived { get; set; }

        public bool IsReceivingLocked { get; set; }

        #endregion

        public ProductData ProductCleared { get; set; }

        public IStatusCode ClearingStatus { get; set; }

        public DateTime? ClearingDate { get; set; }

        public string ClearedBy { get; set; }

        public string ClearingRemarks { get; set; }

        public bool IsClearingEnabled { get; set; }

        public bool IsClearingLocked { get; set; }

        public IForwardingInstruction ForwardingInstruction { get; set; }
    }
}

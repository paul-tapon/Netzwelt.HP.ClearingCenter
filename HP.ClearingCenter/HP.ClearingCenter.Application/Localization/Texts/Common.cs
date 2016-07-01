using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application
{
    public class Text
    {
        public static Text Common { get; set; }

        public string TransportNumber { get; set; }

        public string QuoteNumber { get; set; }

        public string ProgramName { get; set; }

        public string UnitsAdvised { get; set; }

        public string UnitsReceived { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public string PickupDate { get; set; }

        public string ProductName { get; set; }
        
        public string ReceivingDate { get; set; }

        public string ReceivedBy { get; set; }
    }
}

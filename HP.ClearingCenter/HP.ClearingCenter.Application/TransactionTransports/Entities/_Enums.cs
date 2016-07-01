using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    public enum ProgramType 
    {
        Unknown = 0,
        TradeIn = 1,
        BuyAndTry = 2
    }

    public enum ProcessClassification
    {
        Unknown = 0,
        Receiving = 1,
        Clearing = 2,
        Forwarding = 3
    }
}

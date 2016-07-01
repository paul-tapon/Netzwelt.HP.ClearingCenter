using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryResults
{
    public interface IForwardingInstruction
    {
        string ExternalCode { get; }

        string ShortName { get; }

        string Description { get; }

        string TranslatorShortcut { get; }
    }
}

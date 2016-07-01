using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryResults
{
    public interface IStatusCode
    {
        string ExternalCode { get; }

        string ShortName { get; }

        string TranslatorShortcut { get; }
    }
}

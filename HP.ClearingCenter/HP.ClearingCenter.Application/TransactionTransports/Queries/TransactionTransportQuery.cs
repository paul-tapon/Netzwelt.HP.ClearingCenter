using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Queries
{
    public class TransactionTransportQuery : IQuery<IEnumerable<TransactionTransportData>>
    {
        public string Input { get; set; }
    }
}

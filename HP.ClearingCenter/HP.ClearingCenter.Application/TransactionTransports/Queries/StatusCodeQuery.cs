using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Queries
{ 
    public class StatusCodeQuery : IQuery<IEnumerable<IStatusCode>>
    {
        public string ExternalCode { get; set; }

        public bool? IsReceiving { get; set; }

        public bool? IsClearing { get; set; }
    }
}

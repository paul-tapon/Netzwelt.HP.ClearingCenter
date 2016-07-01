using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Queries
{
    public class GetReceivedProductsQuery : IQuery<TransportProcessData[]>
    {
        public GetReceivedProductsQuery()
        {
            this.MaxRows = 100;
        }

        public int MaxRows { get; set; }

        public int MarketingProgramId { get; set; }

        public string CountryIsoCode { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsReceivingSuccessful { get; set; }
    }
}

using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.Transactions
{
    public class TransactionsSearchViewModel
    {
        private IQueryDispatcher query;
        private string searchText;

        public TransactionsSearchViewModel(IQueryDispatcher query, string searchText)            
        {
            this.TransactionTransports = Enumerable.Empty<TransactionTransportData>();
            this.query = query;
            this.searchText = searchText;
        }

        public IEnumerable<TransactionTransportData> TransactionTransports { get; private set; }

        public TransactionsSearchViewModel Initialize()
        {
            if (query != null)
            {
                this.TransactionTransports = this.query
                    .Dispatch(new TransactionTransportQuery { Input = this.searchText });
            }

            return this;
        }
    }


}
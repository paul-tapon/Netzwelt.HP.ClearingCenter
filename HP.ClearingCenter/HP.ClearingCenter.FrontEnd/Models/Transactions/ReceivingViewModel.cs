using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models.Transactions
{
    public class ReceivingViewModel
    {
        private IQueryDispatcher query;
        
        public ReceivingViewModel(IQueryDispatcher query)
        {
            this.query = query;
        }

        public TransactionTransportData TransactionTransport { get; private set; }

        public ReceiveProductCommand ReceiveProductCommand { get; private set; }

        public IEnumerable<SelectListItem> ValidQuantities {
            get
            {
                yield return new SelectListItem { Value = "0", Text = "0", Selected = true };
                if (this.TransactionTransport != null)
                {
                    int maxQuantity = this.TransactionTransport.UnitsAdvised * 5;
                    if (maxQuantity < 1) maxQuantity = 10;

                    for (int idx = 1; idx <= maxQuantity; idx++)
                    {
                        yield return new SelectListItem { Value = idx.ToString(), Text = idx.ToString() };
                    }
                }
            }
        }

        public IEnumerable<SelectListItem> StatusCodes { get; private set; }

        public ReceivingViewModel Initialize(string transportNumber)
        {
            this.TransactionTransport = this.query
                .Dispatch(new TransactionTransportQuery { Input = transportNumber })
                .FirstOrDefault();

            if (this.TransactionTransport != null)
            {
                this.ReceiveProductCommand = new ReceiveProductCommand
                {
                    TransportNumber = TransactionTransport.TransportNumber,
                    UnitsReceived = TransactionTransport.UnitsReceived.GetValueOrDefault(),
                    Remarks = TransactionTransport.ReceivingRemarks,
                    StatusCode = TransactionTransport.ReceivingStatus.ExternalCode
                };
                
                this.StatusCodes = this.query
                    .Dispatch(new StatusCodeQuery { IsReceiving = true })
                    .Select(x => new SelectListItem {
                        Value = x.ExternalCode,
                        Text = x.ShortName,
                        Selected = x.ExternalCode == this.TransactionTransport.ReceivingStatus.ExternalCode
                    });
            }

            return this;
        }
    }
}
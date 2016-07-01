using HP.ClearingCenter.Application.Configuration;
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
    public class ClearingViewModel
    {
        private IQueryDispatcher query;

        public ClearingViewModel(IQueryDispatcher query)
        {
            this.query = query;
        }

        public string SuccessfulClearingStatusCode
        {
            get
            {
                return DomainSettings.Current.SuccessfulClearingStatusCode;
            }
        }

        public IEnumerable<SelectListItem> StatusCodes { get; private set; }

        public IEnumerable<SelectListItem> ForwardingInstructions { get; private set; }

        public TransactionTransportData TransactionTransport { get; private set; }

        public ClearReceivedProductCommand ClearReceivedProductCommand { get; private set; }

        public ClearingViewModel Initialize(string transportNumber)
        {
            this.TransactionTransport = this.query
                .Dispatch(new TransactionTransportQuery { Input = transportNumber })
                .FirstOrDefault();

            if (this.TransactionTransport != null)
            {
                this.ClearReceivedProductCommand = new ClearReceivedProductCommand
                {
                    TransportNumber = this.TransactionTransport.TransportNumber,
                    SelectedProduct = this.TransactionTransport.ProductCleared,
                    ClearingRemarks = this.TransactionTransport.ClearingRemarks,
                    ClearingSelectedProduct = this.TransactionTransport.ProductCleared != null ? this.TransactionTransport.ProductCleared.GetDisplayText() : string.Empty,
                    ClearingStatus = this.TransactionTransport.ClearingStatus != null ? this.TransactionTransport.ClearingStatus.ExternalCode : null,
                    ForwardingInstructionExternalCode = this.TransactionTransport.ForwardingInstruction != null ? this.TransactionTransport.ForwardingInstruction.ExternalCode : null
                };

                this.ForwardingInstructions = this.query
                    .Dispatch(new ForwardingInstructionsQuery())
                    .Select(x => new SelectListItem
                    {
                        Value = x.ExternalCode,
                        Text = x.ShortName,
                        Selected = this.TransactionTransport.ForwardingInstruction != null ? (x.ExternalCode == this.TransactionTransport.ForwardingInstruction.ExternalCode) : false
                    });

                this.StatusCodes = this.query
                    .Dispatch(new StatusCodeQuery { IsClearing = true })
                    .Select(x => new SelectListItem
                    {
                        Value = x.ExternalCode,
                        Text = x.ShortName,
                        Selected = x.ExternalCode == this.TransactionTransport.ClearingStatus.ExternalCode
                    });
            }

            return this;
        }
    }
}
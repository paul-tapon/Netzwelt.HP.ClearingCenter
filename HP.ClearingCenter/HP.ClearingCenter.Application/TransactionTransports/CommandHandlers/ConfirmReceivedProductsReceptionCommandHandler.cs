using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Application.TransactionTransports.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.CommandHandlers
{
    public class ConfirmReceivedProductsReceptionCommandHandler : ICommandHandler<ConfirmReceivedProductsReceptionCommand>
    {
        public void Execute(ICommandContext<ConfirmReceivedProductsReceptionCommand> context)
        {
            string[] transportIds = context.Args.TransportIds ?? new string[0];

            foreach (var transportId in transportIds)
            {
                using (var db = new ClearingCenterDataContext())
                {
                    var transport = this.GetTransport(transportId, context, db);
                    if (transport == null) return;
                    
                    this.ConfirmReception(transport, context);
                    db.SaveChanges();
                }
            }
        }

        private TransactionDetail GetTransport(string transportId, ICommandContext<ConfirmReceivedProductsReceptionCommand> context, ClearingCenterDataContext db)
        {
            var transport = db.TransactionDetails
                .FirstOrDefault(x => 
                    x.TransportNumber == transportId &&
                    x.ReceivingDate != null &&
                    x.ReceivingStatus != null);

            if (transport == null)
            {
                context.ReportError("Unable to find the transport with ID '{0}'".WithTokens(transportId));
            }

            return transport;
        }

        private void ConfirmReception(TransactionDetail transport, ICommandContext<ConfirmReceivedProductsReceptionCommand> context)
        {   
            if (transport.ReceivingConfirmationDate.HasValue)
            {
                context.ReportError("Transport with ID {0} was already confirmed on {1} by {2}"
                    .WithTokens(transport.TransportNumber, transport.ReceivingConfirmationDate, transport.ReceivingConfirmedBy));

                return;
            }

            transport.ReceivingConfirmationDate = context.Args.ReceivingConfirmationDate;
            transport.ReceivingConfirmedBy = context.Args.ReceivingConfirmationUsername;
        }
    }
}

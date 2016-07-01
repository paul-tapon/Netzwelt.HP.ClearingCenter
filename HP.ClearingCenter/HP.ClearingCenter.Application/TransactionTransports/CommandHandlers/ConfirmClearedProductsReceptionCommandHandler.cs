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
    public class ConfirmClearedProductsReceptionCommandHandler : ICommandHandler<ConfirmClearedProductsReceptionCommand>
    {
        public void Execute(ICommandContext<ConfirmClearedProductsReceptionCommand> context)
        {
            string[] transportIds = context.Args.TransportIds ?? new string[0];

            foreach (var transportNumber in transportIds)
            {
                using (var db = new ClearingCenterDataContext())
                {
                    var transport = this.GetTransport(transportNumber, context, db);
                    if (transport == null) return;

                    this.ConfirmReception(transport, context);
                    db.SaveChanges();
                }
            }
        }

        private TransactionDetail GetTransport(string transportId, ICommandContext<ConfirmClearedProductsReceptionCommand> context, ClearingCenterDataContext db)
        {
            var transport = db.TransactionDetails
                .FirstOrDefault(x =>
                    x.TransportNumber == transportId &&
                    x.ReceivingDate != null &&
                    x.ReceivingStatus != null &&
                    x.ReceivingConfirmationDate != null &&
                    x.ClearingDate != null &&
                    x.ClearingStatus != null);

            if (transport == null)
            {
                context.ReportError("Unable to find the transport with ID '{0}'".WithTokens(transportId));
            }

            return transport;
        }

        private void ConfirmReception(TransactionDetail transport, ICommandContext<ConfirmClearedProductsReceptionCommand> context)
        {
            if (transport.ClearingConfirmationDate.HasValue)
            {
                context.ReportError("Transport with ID {0} was already confirmed on {1} by {2}"
                    .WithTokens(transport.TransportNumber, transport.ClearingConfirmationDate, transport.ClearingConfirmedBy));

                return;
            }

            transport.ClearingConfirmationDate = context.Args.ClearingConfirmationDate;
            transport.ClearingConfirmedBy = context.Args.ClearingConfirmationUsername;
        }
    }
}

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
    public class ReceiveProductCommandHandler : ICommandHandler<ReceiveProductCommand>
    {
        private IApplicationContext app;

        public ReceiveProductCommandHandler(IApplicationContext app)
        {
            this.app = app;
        }
        
        public void Execute(ICommandContext<ReceiveProductCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var adapter = new TransactionTransportsDataAdapter(db);
                this.ExecuteCommand(context, adapter);
            }
        }

        private void ExecuteCommand(ICommandContext<ReceiveProductCommand> context, TransactionTransportsDataAdapter db)
        {
            var transport = db.FindSingleTransactionDetail(context.Args.TransportNumber);
            var status = db.FindSingleStatusCode(context.Args.StatusCode);
            
            transport.ReceivingDate = this.app.GetUtcNow();
            transport.UnitsReceived = context.Args.UnitsReceived;
            transport.ReceivingStatus = status;
            transport.ReceivedBy = this.app.GetCurrentUser().Username;
            transport.ReceivingDate = this.app.GetUtcNow();
            transport.ReceivingRemarks = context.Args.Remarks;

            db.SaveChanges();
        }
    }
}

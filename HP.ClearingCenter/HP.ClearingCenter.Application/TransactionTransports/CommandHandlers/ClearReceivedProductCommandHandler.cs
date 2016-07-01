using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Entities;
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
    public class ClearReceivedProductCommandHandler : ICommandHandler<ClearReceivedProductCommand>
    {
        private IApplicationContext app;

        public ClearReceivedProductCommandHandler(IApplicationContext app)
        {
            this.app = app;
        }

        public void Execute(ICommandContext<ClearReceivedProductCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var adapter = new TransactionTransportsDataAdapter(db);
                this.ExecuteCommand(context, adapter);
            }
        }

        public virtual void ExecuteCommand(ICommandContext<ClearReceivedProductCommand> context, TransactionTransportsDataAdapter db)
        {
            Protect.AgainstInvalidOperation(context.Args.SelectedProduct.IsNull(),
                "SelectedProduct cannot be null.");

            Product product = db.FindProductById(context.Args.SelectedProduct.ProductId.GetValueOrDefault(-1));
            Protect.AgainstInvalidOperation(product.IsNull(),
                "Invalid product id selected: ".WithTokens(context.Args.SelectedProduct.ProductId));

            var td = db.FindSingleTransactionDetail(context.Args.TransportNumber);
            var statusCode = db.FindSingleStatusCode(context.Args.ClearingStatus);
            var forwardingInstructions = db.FindSingleForwardingInstruction(context.Args.ForwardingInstructionExternalCode);

            td.ClearedBy = this.app.GetCurrentUser().Username;
            td.ClearingDate = this.app.GetUtcNow();
            td.ClearingStatus = statusCode;
            td.ForwardingInstruction = forwardingInstructions;
            td.ClearingRemarks = context.Args.ClearingRemarks;
            
            // product info
            td.ManufacturerNameReceived = context.Args.SelectedProduct.Manufacturer;
            td.ProductNameReceived = product.ShortName;
            td.ProductNumberReceived = product.ProductNumber;
            td.ProductId = product.Id;

            db.SaveChanges();
        }
    }
}

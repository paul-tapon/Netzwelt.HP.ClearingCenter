using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Commands
{
    public class ConfirmReceivedProductsReceptionCommand : ICommand
    {
        public ConfirmReceivedProductsReceptionCommand()
        {
            this.TransportIds = new string[0];
        }

        public DateTime ReceivingConfirmationDate { get; set; }

        public string ReceivingConfirmationUsername { get; set; }

        public string[] TransportIds { get; set; }
    }
}

using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Commands
{
    public class ConfirmClearedProductsReceptionCommand : ICommand
    {
        public string[] TransportIds { get; set; }

        public DateTime ClearingConfirmationDate { get; set; }

        public string ClearingConfirmationUsername { get; set; }
    }
}

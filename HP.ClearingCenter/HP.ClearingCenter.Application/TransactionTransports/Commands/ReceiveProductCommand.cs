using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Commands
{
    public class ReceiveProductCommand : ICommand
    {
        [Required]
        public string TransportNumber { get; set; }

        public int UnitsReceived { get; set; }

        [Required]
        public string StatusCode { get; set; }

        public string Remarks { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string ReceivedBy { get; set; }
    }
}

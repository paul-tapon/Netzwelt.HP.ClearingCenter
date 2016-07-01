using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Commands
{
    public class ClearReceivedProductCommand : ICommand
    {
        [Required]
        public string TransportNumber { get; set; }

        public ProductData SelectedProduct { get; set; }

        [Required]
        public string ClearingSelectedProduct { get; set; }

        public string ClearingStatus { get; set; }

        public string ClearingRemarks { get; set; }

        public string ForwardingInstructionExternalCode { get; set; }
    }
}

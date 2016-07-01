using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Commands
{
    public class AdviseClearingCenterProductCommand : ICommand
    {
        [Required]
        public Order Order { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

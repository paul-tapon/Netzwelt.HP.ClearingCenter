using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Queries
{
    public class ValidateClearingProductQuery : IQuery<ValidateClearingProductResult>
    {
        [Required]
        [StringLength(128)]
        public string TransportNumber { get; set; }

        public int SelectedProductId { get; set; }
    }
}

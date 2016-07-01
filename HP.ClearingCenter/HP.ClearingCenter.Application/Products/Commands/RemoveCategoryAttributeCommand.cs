using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Commands
{
    public class RemoveCategoryAttributeCommand : ICommand
    {
        public int CategoryId { get; set; }

        [Required]
        public string CustomAttributeExternalCode { get; set; }
    }
}

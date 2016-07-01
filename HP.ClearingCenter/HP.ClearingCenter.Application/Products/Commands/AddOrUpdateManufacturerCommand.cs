using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Commands
{
    public class AddOrUpdateManufacturerCommand : ICommand
    {
        public int? ManufacturerId { get; set; }

        [Required]
        [StringLength(128)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Shortname { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}

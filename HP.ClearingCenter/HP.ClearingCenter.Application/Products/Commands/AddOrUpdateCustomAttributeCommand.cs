using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Commands
{
    public class AddOrUpdateCustomAttributeCommand : ICommand
    {
        public int CustomAttributeId { get; set; }
        
        [Required]
        [StringLength(128)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Shortname { get; set; }

        [StringLength(64)]
        public string UnitText { get; set; }

        public string CustomAttributeTypeId { get; set; }

        [StringLength(128)]
        public string TranslatorShortcut { get; set; }
    }
}

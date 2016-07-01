using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Commands
{
    public class AddProductGroupCommand : ICommand
    {
        public int? ProductGroupId { get; set; }
        
        [Required]
        public int? MarketingProgramId { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string CountryIsoCode { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3)]
        public string ShortName { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}

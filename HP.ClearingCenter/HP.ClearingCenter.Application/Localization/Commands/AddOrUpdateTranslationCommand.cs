using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Localization.Commands
{
    public class AddOrUpdateTranslationCommand : ICommand
    {
        [Required]
        [StringLength(3)]
        public string CountryIsoCode { get; set; }

        [Required]
        [StringLength(3)]
        public string LanguageIsoCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ResourceKey { get; set; }

        [MaxLength(4096)]
        public string TextFormat { get; set; }
    }
}

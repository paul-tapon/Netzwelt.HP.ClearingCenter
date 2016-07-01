using HP.ClearingCenter.Application.Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Entities
{
    [Table("T_User")]
    public class ApplicationUser
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(128)]
        public string Username { get; set; }

        public PasswordData Password { get; set; }

        [StringLength(512)]
        public string ApiKey { get; set; }

        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(3)]
        public string DefaultLanguageIsoCode { get; set; }

        public bool IsApiAccessEnabled { get; set; }

        public bool IsLoginEnabled { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsReceivingEnabled { get; set; }

        public bool IsClearingEnabled { get; set; }

        public bool IsProductManagementEnabled { get; set; }
    }
}

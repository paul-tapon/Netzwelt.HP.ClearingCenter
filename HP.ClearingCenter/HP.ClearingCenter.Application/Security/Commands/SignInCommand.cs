using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Security.Commands
{
    public class SignInCommand : ICommand
    {
        [Required]
        [StringLength(128, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

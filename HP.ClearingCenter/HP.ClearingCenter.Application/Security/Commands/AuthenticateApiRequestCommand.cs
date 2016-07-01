using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Security.Commands
{
    public class AuthenticateApiRequestCommand : ICommand
    {
        [Required]
        [MaxLength(128)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string ApiKey { get; set; }
    }
}

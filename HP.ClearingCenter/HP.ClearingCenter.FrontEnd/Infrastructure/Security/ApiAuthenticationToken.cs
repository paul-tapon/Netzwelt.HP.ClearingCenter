using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Security
{
    public class ApiAuthenticationToken : SoapHeader
    {
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class EchoRequest
    {
        public string Message { get; set; }
    }
}

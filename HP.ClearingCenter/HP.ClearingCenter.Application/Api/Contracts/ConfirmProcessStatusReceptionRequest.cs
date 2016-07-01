using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class ConfirmProcessStatusReceptionRequest
    {
        public string[] TransportIds { get; set; }
    }
}

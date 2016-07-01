using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class CustomAttribute
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Configuration
{
    public class DomainSettings
    {
        static DomainSettings()
        {
            Current = new DomainSettings
            {
                SuccessfulClearingStatusCode = Domain.Default.SuccessfulClearingStatusCode
            };
        }

        public static DomainSettings Current { get; private set; }

        public string SuccessfulClearingStatusCode { get; private set; }

    }
}

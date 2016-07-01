using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{   
    public interface IApplicationUser
    {
        string Username { get; }

        string Nickname { get; }

        CultureInfo Culture { get; }

        TimeZoneInfo Timezone { get; }
    }
}

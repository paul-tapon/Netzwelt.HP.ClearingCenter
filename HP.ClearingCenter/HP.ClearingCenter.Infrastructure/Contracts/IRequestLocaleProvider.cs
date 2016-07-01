using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IRequestLocaleProvider
    {
        RequestLocale GetCurrentLocale();
        CultureInfo GetLocaleCulture();
        CultureInfo GetLocaleCulture(RequestLocale locale);
    }
}

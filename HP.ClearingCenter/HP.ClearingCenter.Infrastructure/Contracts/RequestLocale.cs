using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public class RequestLocale
    {

        public RequestLocale() { }

        public RequestLocale(string countryIsoCode, string languageIsoCode)
        {
            CountryIsoCode = countryIsoCode;
            LanguageIsoCode = languageIsoCode;
        }

        public virtual string CountryIsoCode { get; private set; }
        public virtual string LanguageIsoCode { get; private set; }

        public override string ToString()
        {
            return "{0}-{1}".WithTokens(LanguageIsoCode.ToLowerInvariant(), CountryIsoCode.ToUpperInvariant());
        }
    }
}

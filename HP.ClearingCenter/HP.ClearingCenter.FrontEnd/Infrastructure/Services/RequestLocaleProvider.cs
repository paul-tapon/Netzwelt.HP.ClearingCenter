using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
    public class RequestLocaleProvider : IRequestLocaleProvider
    {
        const string CURRENT_LOCALE_KEY = "__CCCurrentRequestLocale";
        
        private Func<HttpContextBase> httpContextFactory;

        public RequestLocaleProvider(Func<HttpContextBase> httpContextFactory)
        {
            this.httpContextFactory = httpContextFactory;
        }

        public RequestLocale CurrentLocale 
        {
            get
            {
                return this.httpContextFactory().Items.Contains(CURRENT_LOCALE_KEY) ?
                    (RequestLocale)httpContextFactory().Items[CURRENT_LOCALE_KEY] : null;
            }
            private set
            {
                this.httpContextFactory().Items[CURRENT_LOCALE_KEY] = value;
            }
        }

        public RequestLocale GetCurrentLocale()
        {
            if (this.CurrentLocale == null)
            {
                var rd = this.httpContextFactory().Request.RequestContext.RouteData.Values;
                string country = this.GetIsoCode(rd, LocalProgramRoute.COUNTRY, "us");
                string language = this.GetIsoCode(rd, LocalProgramRoute.LANGUAGE, "en");

                this.CurrentLocale = new RequestLocale(country, language);
            }

            return this.CurrentLocale;
        }

        public CultureInfo GetLocaleCulture()
        {
            return this.GetLocaleCulture(this.GetCurrentLocale());
        }

        public CultureInfo GetLocaleCulture(RequestLocale locale)
        {
            try
            {
                return CultureInfo.GetCultureInfo(locale.LanguageIsoCode);
            }
            catch
            {
                return CultureInfo.GetCultureInfo("en");
            }
        }

        private string GetIsoCode(RouteValueDictionary routeDataValues, string key, string fallback)
        {
            string result = routeDataValues.ContainsKey(key) ?
                routeDataValues[key].ToString() : fallback;

            Protect.AgainstInvalidOperation(result.Length > 3, "Invalid ISO Code.");

            return result;
        }
    }
}
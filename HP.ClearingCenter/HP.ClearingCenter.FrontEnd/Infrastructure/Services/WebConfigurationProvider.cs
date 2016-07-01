using HP.ClearingCenter.Application.Services;
using HP.ClearingCenter.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
    public class WebConfigurationProvider : ConfigurationProvider
    {
        private HttpContextBase httpContext;

        public WebConfigurationProvider(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public override bool IsShortcutEditModeEnabled
        {
            get
            {
                return this.SessionData.IsShortcutEditModeEnabled;
            }
        }

        public override SessionData SessionData
        {
            get
            {
                const string SESSION_DATA_KEY = "_SessionData";
                if (this.httpContext.Session[SESSION_DATA_KEY] == null)
                {
                    this.httpContext.Session[SESSION_DATA_KEY] = new SessionData();
                }

                return (SessionData)this.httpContext.Session[SESSION_DATA_KEY];
            }
        }
    }
}
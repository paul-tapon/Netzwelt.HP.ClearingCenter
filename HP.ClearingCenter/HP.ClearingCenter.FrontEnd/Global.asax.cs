using HP.ClearingCenter.FrontEnd.App_Start;
using HP.ClearingCenter.FrontEnd.Infrastructure.Services;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HP.ClearingCenter.FrontEnd
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static Func<IApplicationContext> ApplicationContextFactory = () => DependencyResolver.Current.GetService<IApplicationContext>(); 

        protected void Application_Start(object sender, EventArgs e)
        {
            DataConfig.Initialize();
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ProvidersConfig.RegisterServices(DependencyResolver.Current);
        }

        protected void xxxApplication_BeginRequest(object sender, EventArgs e)
        {   
            var currentLocale = new RequestLocaleProvider(() => new HttpContextWrapper(HttpContext.Current))
                .GetCurrentLocale();

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }
    }
}
using HP.ClearingCenter.Application.Security.Services;
using HP.ClearingCenter.Application.Services;
using HP.ClearingCenter.FrontEnd.Infrastructure.Services;
using HP.ClearingCenter.Infrastructure.Contracts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.App_Start
{
    public static class ServicesConfig
    {
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAuthenticationProvider>().To<FormsAuthenticationProvider>();            
            kernel.Bind<ITranslationProvider>().To<TranslationProvider>();
            kernel.Bind<IRequestLocaleProvider>().To<RequestLocaleProvider>();
            kernel.Bind<IConfigurationProvider>().To<WebConfigurationProvider>();
            kernel.Bind<IDebugSettingsProvider>().To<DebugSettingsProvider>();
            kernel.Bind<IAuthorizationProvider>().To<AuthorizationProvider>();
            kernel.Bind<IApplicationContext>().To<ApplicationContext>().WithConstructorArgument("kernel", kernel);
        } 
    }
}
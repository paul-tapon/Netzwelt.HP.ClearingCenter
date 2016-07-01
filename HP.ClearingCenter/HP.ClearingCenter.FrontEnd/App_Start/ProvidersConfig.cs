using HP.ClearingCenter.FrontEnd.Infrastructure.Services;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.App_Start
{
    public class ProvidersConfig
    {
        public static void RegisterServices(IDependencyResolver dependencies)
        {
            //return;
            ITranslationProvider translationProvider = dependencies.GetService<ITranslationProvider>();
            IRequestLocaleProvider requestLocaleProvider = dependencies.GetService<IRequestLocaleProvider>();

            
            ModelMetadataProviders.Current = new LocalizedModelMetadataProvider(translationProvider, requestLocaleProvider);
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new LocalizedModelValidatorProvider(translationProvider, requestLocaleProvider, new ClientSideValidationRuleFactory()));
        }
    }
}
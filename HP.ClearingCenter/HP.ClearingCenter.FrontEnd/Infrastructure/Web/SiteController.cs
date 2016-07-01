using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public abstract class SiteController : Controller
    {
        public SiteController(IApplicationContext context)
            : base()
        {
            this.Application = context;
        }

        public virtual IApplicationContext Application { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.UpdateDebugParameters(filterContext);
            base.OnActionExecuting(filterContext);
        }

        protected virtual TResult PerformQuery<TResult>(IQuery<TResult> args) 
        {
            return this.Application.PerformQuery(args);
        }

        protected virtual CommandResult ProcessCommand<TCommand>(TCommand args) where TCommand : class, ICommand
        {   
            return this.Application.ExecuteCommand<TCommand>(args, new MvcDataAnnotationsValidationProvider(this.ModelState));
        }

        private void UpdateDebugParameters(ActionExecutingContext filterContext)
        {
            var queryParams = filterContext.RequestContext.HttpContext.Request.QueryString;
            var debugSettings = this.Application.DebugSettingsProvider.GetCurrentSettings();
            bool shortcutEditMode = debugSettings.IsShortcutEditModeEnabled;
            bool translatorCachingEnabled = debugSettings.IsTranslatorCachingEnabled;            
            bool settingsChanged = false;

            if (queryParams.AllKeys.Contains("shortcutEditMode"))
            {
                bool.TryParse(queryParams["shortcutEditMode"], out shortcutEditMode);
                settingsChanged = true;
            }

            if (queryParams.AllKeys.Contains("translatorCachingEnabled"))
            {
                bool.TryParse(queryParams["translatorCachingEnabled"], out translatorCachingEnabled);
                settingsChanged = true;
            }

            if (settingsChanged)
            {
                // update the debug settings
                
                debugSettings.IsShortcutEditModeEnabled = shortcutEditMode;
                debugSettings.IsTranslatorCachingEnabled = translatorCachingEnabled;
                this.Application.DebugSettingsProvider.UpdateSettings(debugSettings);
            }
        }
    }
}
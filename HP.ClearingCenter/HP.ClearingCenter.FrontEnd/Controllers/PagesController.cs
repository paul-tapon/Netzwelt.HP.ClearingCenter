using HP.ClearingCenter.Application.Localization.Commands;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    [Authorize]
    public class PagesController : SiteController
    {
        public PagesController(IApplicationContext app) : base(app) { }

        public ActionResult Index(string id)
        {
            return HttpNotFound();
        }

        [HttpGet]
        [NoCache]
        public ActionResult LocalizeText(string ciso, string liso, string shortcut)
        {
            UpdateDebugSettings(true, false);

            var token = new TranslationToken(new RequestLocale(ciso, liso), shortcut);
            var item = this.Application.TranslationProvider.GetTranslation(token);

            return View(new AddOrUpdateTranslationCommand
            {
                CountryIsoCode = token.Locale.CountryIsoCode,
                LanguageIsoCode = token.Locale.LanguageIsoCode,
                ResourceKey = token.ResourceKey,
                TextFormat = item.TextFormat                
            });
        }

        [HttpPost]
        public ActionResult LocalizeText(AddOrUpdateTranslationCommand command)
        {
            this.ProcessCommand(command);

            return this.RedirectToAction("LocalizeText", "Pages", new
            {
                ciso = command.CountryIsoCode,
                liso = command.LanguageIsoCode,
                shortcut = command.ResourceKey                
            });
        }

        [HttpGet]
        public ActionResult Debug()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Debug(bool isShortcutEditMode, bool isTranslatorCachingEnabled)
        {
            UpdateDebugSettings(isShortcutEditMode, isTranslatorCachingEnabled);
            return View();
        }

        private void UpdateDebugSettings(bool isShortcutEditMode, bool isTranslatorCachingEnabled)
        {
            var debugSettings = this.Application.DebugSettingsProvider;
            var currentSettings = debugSettings.GetCurrentSettings();
            currentSettings.IsShortcutEditModeEnabled = isShortcutEditMode;
            currentSettings.IsTranslatorCachingEnabled = isTranslatorCachingEnabled;

            debugSettings.UpdateSettings(currentSettings);
        }
    }
}

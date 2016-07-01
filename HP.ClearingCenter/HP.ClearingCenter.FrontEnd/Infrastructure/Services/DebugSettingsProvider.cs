using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
    public class DebugSettingsProvider : IDebugSettingsProvider
    {
        const string DEBUG_SETTINGS_COOKIE_NAME = "__CCDebugSettings";
        
        private Func<HttpContextBase> httpContextFactory;        

        public DebugSettingsProvider(Func<HttpContextBase> httpContextFactory)
        {
            this.httpContextFactory = httpContextFactory;
        }

        private IDebugSettings CurrentSettings
        {
            get
            {
                return this.httpContextFactory().Items.Contains(DEBUG_SETTINGS_COOKIE_NAME) ?
                    (IDebugSettings)this.httpContextFactory().Items[DEBUG_SETTINGS_COOKIE_NAME] : null;
            }
            set
            {
                this.httpContextFactory().Items[DEBUG_SETTINGS_COOKIE_NAME] = value;
            }
        }

        public IDebugSettings GetCurrentSettings()
        {
            if (this.CurrentSettings.Exists()) return this.CurrentSettings;

            var cookie = httpContextFactory().Request.Cookies.AllKeys.Contains(DEBUG_SETTINGS_COOKIE_NAME) ?
                httpContextFactory().Request.Cookies[DEBUG_SETTINGS_COOKIE_NAME] : null;

            this.UpdateSettings(this.Deserialize(cookie));
            return this.CurrentSettings;
        }

        public void UpdateSettings(IDebugSettings settings)
        {
            this.Serialize(settings);
            this.CurrentSettings = settings;
        }

        private void Serialize(IDebugSettings currentSettings)
        {
            //TODO: don't serialize if debug provider is disabled

            string json = currentSettings.ToJson();
            string aesEncrypted = json.AESEncrypt();
            string base64 = aesEncrypted.Base64Encode();
            var cookie = new HttpCookie(DEBUG_SETTINGS_COOKIE_NAME) { Value = base64, Expires = DateTime.UtcNow.AddHours(1) };

            this.httpContextFactory().Response.Cookies.Set(cookie);
        }

        private IDebugSettings Deserialize(HttpCookie cookie)
        {
            if (cookie.IsNull())
            {
                return new DebugSettings
                {
                    IsShortcutEditModeEnabled = false,
                    IsTranslatorCachingEnabled = true
                };
            }

            var base64 = cookie.Value;
            var aesEncrypted = base64.Base64Decode();
            var json = aesEncrypted.AESDecrypt();
            return json.DeserializeFromJsonAs<DebugSettings>();
        }

        [Serializable]
        public class DebugSettings : IDebugSettings
        {
            public bool IsShortcutEditModeEnabled { get; set; }
            public bool IsTranslatorCachingEnabled { get; set; }
        }
    }
}
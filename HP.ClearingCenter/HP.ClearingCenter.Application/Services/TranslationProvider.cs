using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Services
{
    public class TranslationProvider : ITranslationProvider
    {
        private static object syncLock = new object();
        private static IDictionary<string, TranslationItem> cache = new Dictionary<string, TranslationItem>();
        private IRequestLocaleProvider localeProvider;
        private IDebugSettingsProvider debugProvider;

        public TranslationProvider(IRequestLocaleProvider localeProvider, IDebugSettingsProvider debugProvider)
        {
            this.localeProvider = localeProvider;
            this.debugProvider = debugProvider;
        }

        public void SaveTranslation(TranslationToken token, string textFormat)
        {
            throw new NotImplementedException();
        }

        public TranslationItem GetTranslation(TranslationToken token)
        {
            string key = "[{0}]{1}".WithTokens(token.Locale.ToString(), token.ResourceKey);
            bool isCacheEnabled = this.debugProvider.GetCurrentSettings().IsTranslatorCachingEnabled;
            return GetCachedTranslation(key, () => this.FetchTranslation(token), isCacheEnabled) ?? new TranslationItem(token, null);
        }

        public TranslationItem GetTranslation<TModel, TProp>(TModel source, System.Linq.Expressions.Expression<Func<TModel, TProp>> resourceKey, RequestLocale locale, params string[] extras)
        {
            return this.GetTranslation(TranslationToken.DetectFrom<TModel, TProp>(resourceKey, locale, extras));
        }

        private static TranslationItem GetCachedTranslation(string key, Func<TranslationItem> factory, bool isCacheEnabled = true)
        {
            if (!isCacheEnabled) return factory();
            
            if (!cache.ContainsKey(key))
            {
                lock (syncLock)
                {
                    if (!cache.ContainsKey(key))
                    {
                        var translationItem = factory();

                        if (translationItem.Exists() && !translationItem.NoTextAvailable())
                        {
                            cache.Add(key, factory());
                        }
                    }
                }
            }

            return cache.ContainsKey(key) ? cache[key] : null;
        }

        private TranslationItem FetchTranslation(TranslationToken token)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var translator = db.Translators
                    .Where(x =>
                        x.CountryIsoCode == token.Locale.CountryIsoCode &&
                        x.LanguageIsoCode == token.Locale.LanguageIsoCode &&
                        x.Shortcut == token.ResourceKey)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                string key = "[{0}]{1}".WithTokens(token.Locale.ToString(), token.ResourceKey);

                return translator.Exists() ?
                    new TranslationItem(token, translator.TextValue) :
                    new TranslationItem(token, string.Empty);
            }
        }
    }
}

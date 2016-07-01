using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public class TranslationToken
    {
        static TranslationToken()
        {
            Default = new TranslationToken(null, null);
        }

        public static TranslationToken Default { get; private set; }

        public static string FormatResourceKey(Type type, string propertyName, params string[] extras)
        {
            var baseStr = type.Exists() ? string.Format("{0}_{1}", type.Name, propertyName) : propertyName;
            return extras.Aggregate(baseStr, (current, extra) => current + ("_" + extra));
        }

        public static TranslationToken DetectFrom<TModel, TValue>(TModel model, Expression<Func<TModel, TValue>> expression, RequestLocale locale, params string[] extras)
        {
            return DetectFrom<TModel, TValue>(expression, locale);
        }

        public static TranslationToken DetectFrom<TModel, TValue>(Expression<Func<TModel, TValue>> expression, RequestLocale locale, params string[] extras)
        {
            var member = expression.MemberExpression();
            if (member == null)
                return Default;

            return DetectFrom(typeof(TModel), member.Member.Name, locale);
        }

        public static TranslationToken DetectFrom(Type type, string propertyName, RequestLocale requestLocale, params string[] extras)
        {
            return new TranslationToken(requestLocale, FormatResourceKey(type, propertyName, extras));
        }

        public static TranslationToken DetectFrom(string key, RequestLocale requestLocale, params string[] extras)
        {
            string baseStr = key;
            return new TranslationToken(requestLocale, FormatResourceKey(null, key, extras));
        }

        public TranslationToken(RequestLocale locale, string resourceKey)
        {
            Locale = locale;
            ResourceKey = resourceKey;
        }

        public virtual RequestLocale Locale { get; private set; }
        public virtual string ResourceKey { get; private set; }
    }
}

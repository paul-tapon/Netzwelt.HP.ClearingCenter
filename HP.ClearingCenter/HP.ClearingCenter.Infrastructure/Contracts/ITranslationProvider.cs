using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface ITranslationProvider
    {
        void SaveTranslation(TranslationToken token, string textFormat);
        TranslationItem GetTranslation(TranslationToken token);
        TranslationItem GetTranslation<TModel, TProp>(TModel source, Expression<Func<TModel, TProp>> resourceKey, RequestLocale locale, params string[] extras);
    }
}

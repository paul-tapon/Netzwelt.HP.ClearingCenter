using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public class TranslationItem
    {

        static TranslationItem()
        {
            NotFound = new TranslationItem(null, string.Empty);
        }

        public static TranslationItem NotFound { get; private set; }

        public TranslationItem(TranslationToken token, string textFormat)
        {
            Token = token;
            TextFormat = textFormat;
        }

        public virtual TranslationToken Token { get; private set; }
        public virtual string TextFormat { get; private set; }

        public virtual bool NoTextAvailable()
        {
            return TextFormat.IsNullOrEmpty();
        }

        public virtual string GetTranslationKey()
        {
            return Token.IsNull() ? string.Empty : "{0}".WithTokens(Token.ResourceKey);
        }

        public virtual string ToString(params object[] parameters)
        {
            return GetString().WithTokens(parameters);
        }

        public virtual string ShowTranslationKeyWhenNoTextAvailable(params object[] parameters)
        {
            return NoTextAvailable() ? "**{0}**".WithTokens(GetTranslationKey()) :
                ToString(parameters);
        }

        public override string ToString()
        {
            return GetString();
        }

        private string GetString()
        {
            return TextFormat.IsNullOrEmpty() ? string.Empty : TextFormat;
        }
    }
}

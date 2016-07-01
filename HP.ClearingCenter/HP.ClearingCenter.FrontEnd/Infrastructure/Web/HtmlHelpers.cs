using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString Translate<TModel, TRes>(this HtmlHelper html, TModel model, Expression<Func<TModel, TRes>> resourceKey, params object[] parameters)
        {
            // NOTE: this does not support shortcut edit mode. To be used in areas such as page titles            
            var translatedText = GetTranslationItem<TModel, TRes>(model, resourceKey);
            return new MvcHtmlString(ShowLocalizedText(translatedText, parameters));
        }

        public static MvcHtmlString TranslateWithEditor<TModel, TRes>(this HtmlHelper html, TModel model, Expression<Func<TModel, TRes>> resourceKey, params object[] parameters)
        {
            var translatedText = GetTranslationItem<TModel, TRes>(model, resourceKey);
            return GenerateEditorIcon(html, translatedText, new MvcHtmlString(ShowLocalizedText(translatedText, parameters)));
        }
        
        public static MvcHtmlString LocalizedLabelFor<TModel, TRes>(this HtmlHelper html, TModel instance, Expression<Func<TModel, TRes>> resourceKey)
        {
            return LocalizedLabelFor<TModel, TRes>(new HtmlHelper<TModel>(html.ViewContext, html.ViewDataContainer), resourceKey);
        }
        
        public static MvcHtmlString LocalizedLabelFor<TModel, TRes>(this HtmlHelper<TModel> html, Expression<Func<TModel, TRes>> resourceKey) 
        {
            TranslationItem translatedText = GetTranslationItem<TModel, TRes>(default(TModel), resourceKey);

            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("for", resourceKey.MemberExpression<TModel, TRes>().Member.Name);
            label.SetInnerText(ShowLocalizedText(translatedText));

            return GenerateEditorIcon(html, translatedText, new MvcHtmlString(label.ToString()));
        }

        public static MvcHtmlString LocalizedButton<TModel, TRes>(this HtmlHelper html, TModel model, Expression<Func<TModel, TRes>> resourceKey, object htmlAttributes = null, params object[] parameters)
        {
            var translatedText = GetTranslationItem<TModel, TRes>(model, resourceKey);

            var button = new TagBuilder("input");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("value", ShowLocalizedText(translatedText, parameters));

            if (htmlAttributes.Exists())
            {
                button.MergeAttributes(htmlAttributes.ToKeyValues());
            }

            return GenerateEditorIcon(html, translatedText, new MvcHtmlString(button.ToString()));
        }

        public static MvcHtmlString SubmitButton<TModel, TRes>(this HtmlHelper html, TModel model, Expression<Func<TModel, TRes>> resourceKey, object attributes = null, params object[] parameters)
        {
            var translatedText = GetTranslationItem<TModel, TRes>(model, resourceKey);

            var button = new TagBuilder("input");
            button.Attributes.Add("type", "submit");
            button.Attributes.Add("value", ShowLocalizedText(translatedText, parameters));

            if (attributes.Exists())
            {
                button.MergeAttributes(attributes.ToKeyValues());
            }

            return GenerateEditorIcon(html, translatedText, new MvcHtmlString(button.ToString()));
        }

        public static MvcHtmlString LocalizedLink<TModel, TRes>(this HtmlHelper html, TModel model,
            Expression<Func<TModel, TRes>> resourceKey, string actionName, string controllerName,
            object routeValues = null, object htmlAttributes = null)
        {
            var translatedText = GetTranslationItem<TModel, TRes>(model, resourceKey);

            string url = new UrlHelper(html.ViewContext.RequestContext)
                .Action(actionName, controllerName, routeValues);

            var link = CreateLocalizedLink(translatedText, url);
            if (htmlAttributes.Exists())
            {
                link.MergeAttributes(htmlAttributes.ToKeyValues());
            }

            return GenerateEditorIcon(html, translatedText, new MvcHtmlString(link.ToString()));
        }

        public static HierarchicalDropdownList HierarchicalDropdownList(this HtmlHelper html)
        {
            return new HierarchicalDropdownList();
        }

        public static IApplicationContext GetAppContext(this HtmlHelper html)
        {
            return AppContext();
        }

        public static IEnumerable<string> DebugHelperTexts(this HtmlHelper html)
        {
#if DEBUG
            yield return "Administrator Password: g4C8P$JKPBkaNxO";
#else
            yield break;
#endif
        }

        private static TagBuilder CreateLocalizedLink(TranslationItem translatedText, string url)
        {
            var link = new TagBuilder("a");
            link.Attributes.Add("href", url);
            link.InnerHtml = ShowLocalizedText(translatedText);
            return link;
        }

        private static TranslationItem GetTranslationItem<TModel, TRes>(TModel model, Expression<Func<TModel, TRes>> resourceKey)
        {
            var locale = AppContext().LocaleProvider.GetCurrentLocale();
            var token = TranslationToken.DetectFrom<TModel, TRes>(resourceKey, locale);
            ITranslationProvider translator = AppContext().TranslationProvider;
            var translatedText = translator.GetTranslation(token);
            return translatedText;
        }

        private static string ShowLocalizedText(TranslationItem translation, params object[] parameters)
        {
            if (translation.NoTextAvailable())
            {
                return AppContext().DebugSettingsProvider.GetCurrentSettings().IsShortcutEditModeEnabled ?
                    "**{0}**".WithTokens(translation.GetTranslationKey()) :
                    string.Empty;
            }

            return translation.ToString(parameters);
        }

        private static MvcHtmlString GenerateEditorIcon(HtmlHelper html, TranslationItem item, MvcHtmlString localizedText)
        {
            if (!AppContext().DebugSettingsProvider.GetCurrentSettings().IsShortcutEditModeEnabled)
            {
                return localizedText;
            }
            
            TagBuilder span = new TagBuilder("span");

            StringBuilder innerText = new StringBuilder();
            innerText.Append(localizedText.ToString());
            innerText.Append(GenerateEditorUrl(html, item));
            span.InnerHtml = innerText.ToString();

            return new MvcHtmlString(span.ToString());             
        }

        private static MvcHtmlString GenerateEditorUrl(HtmlHelper html, TranslationItem item)
        {
            // <a href="..."> <img alt="edit" src="..." /> </a>
            UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);            

            TagBuilder editor = new TagBuilder("a");
            string editorUrl = url.Action("LocalizeText", "Pages", new { 
                ciso = item.Token.Locale.CountryIsoCode, 
                liso = item.Token.Locale.LanguageIsoCode, 
                shortcut = item.Token.ResourceKey 
            });

            editor.Attributes.Add("href", editorUrl);
            editor.Attributes.Add("class", "shortcut-editor");
            editor.InnerHtml = "&nbsp; edit";

            return new MvcHtmlString(editor.ToString());
        }

        private static IApplicationContext AppContext()
        {
            return DependencyResolver.Current.GetService<IApplicationContext>();
        }
    }
}
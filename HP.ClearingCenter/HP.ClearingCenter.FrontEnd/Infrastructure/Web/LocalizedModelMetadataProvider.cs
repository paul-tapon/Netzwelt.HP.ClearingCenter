using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    /// <summary>
    /// Metadata provider used to localize models and their meta data.
    /// </summary>
    public class LocalizedModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly ITranslationProvider _translationProvider;
        private readonly IRequestLocaleProvider _localeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedModelMetadataProvider"/> class.
        /// </summary>
        /// <param name="stringProvider">The string provider.</param>
        public LocalizedModelMetadataProvider(ITranslationProvider translationProvider, IRequestLocaleProvider localeProvider)
        {
            _translationProvider = translationProvider;
            _localeProvider = localeProvider;
        }

        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// The metadata for the property.
        /// </returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (containerType == null || propertyName == null)
                return metadata;

            if (metadata.DisplayName == null)
            {
                metadata.DisplayName = Translate(containerType, propertyName);
            }

            if (metadata.Watermark == null)
                metadata.Watermark = Translate(containerType, propertyName, "Watermark");

            if (metadata.Description == null)
                metadata.Description = Translate(containerType, propertyName, "Description");

            if (metadata.NullDisplayText == null)
                metadata.NullDisplayText = Translate(containerType, propertyName, "NullDisplayText");

            if (metadata.ShortDisplayName == null)
                metadata.ShortDisplayName = Translate(containerType, propertyName, "ShortDisplayName");

            return metadata;
        }

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">mode type</param>
        /// <param name="propertyName">Property name to translate</param>
        /// <returns>Translated string</returns>
        protected virtual string Translate(Type type, string propertyName)
        {
            var token = TranslationToken.DetectFrom(type, propertyName, _localeProvider.GetCurrentLocale());
            var translation = _translationProvider.GetTranslation(token);
            return translation.NoTextAvailable() ? token.ResourceKey : translation.ToString();
        }

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">Model type</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="metadataName">Meta data name</param>
        /// <returns>Translated string</returns>
        protected virtual string Translate(Type type, string propertyName, string metadataName)
        {
            var token = TranslationToken.DetectFrom(type, "{0}_{1}".WithTokens(propertyName, metadataName), _localeProvider.GetCurrentLocale());
            var translation = _translationProvider.GetTranslation(token);
            return translation.NoTextAvailable() ? token.ResourceKey : translation.ToString();
        }
    }
}
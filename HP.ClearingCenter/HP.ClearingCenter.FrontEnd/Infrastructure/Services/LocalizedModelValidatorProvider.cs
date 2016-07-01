using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
/// <summary>
    /// Used to localize DataAnnotation attribute error messages and view models
    /// </summary>
    /// <remarks>
    /// <para>Hacks the attributes by assigning custom (localized) messages to them to get localized error messages.</para>
    /// <para>
    /// Check for namespace documentation for an example on how to use the provider.
    /// </para>
    /// <para>Are you missing validation rules for an attribute? Do not try to use the original validation rules. The standard attributes
    /// uses some nasty delegates to handle the error message. Screwing with them should be handled with care. 
    /// </para>
    /// <para>Create a new <see cref="IValidationMessageDataSource"/> and register it in <see cref="ValidationMessageProviders"/> to customized the translated strings.</para>
    /// <para>You have to let the results returned from <c>Validate()</c> implement <see cref="IClientValidationRule"/> if you want to enable client validation when using <see cref="IValidatableObject"/>.</para>
    /// </remarks>
    public class LocalizedModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        private readonly ITranslationProvider _translationProvider;
        private readonly IRequestLocaleProvider _requestLocaleProvider;
        private readonly ClientSideValidationRuleFactory _clientValidationRuleFactory;

        public LocalizedModelValidatorProvider(ITranslationProvider translationProvider,
            IRequestLocaleProvider requestLocaleProvider, ClientSideValidationRuleFactory clientValidationRuleFactory)
        {

            _translationProvider = translationProvider;
            _requestLocaleProvider = requestLocaleProvider;
            _clientValidationRuleFactory = clientValidationRuleFactory;
        }

        /// <summary>
        /// Gets a list of validators.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attributes">The list of validation attributes.</param>
        /// <returns>
        /// A list of validators.
        /// </returns>
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context,
                                                                     IEnumerable<Attribute> attributes)
        {
            var items = attributes.ToList();
            if (AddImplicitRequiredAttributeForValueTypes && metadata.IsRequired &&
                !items.Any(a => a is RequiredAttribute))
                items.Add(new RequiredAttribute());

            var validators = new List<ModelValidator>();
            foreach (var attr in items.OfType<ValidationAttribute>())
            {

                var translationToken = new TranslationToken(_requestLocaleProvider.GetCurrentLocale(),
                    "Error_{0}".WithTokens(attr.GetType().Name));

                var translationItem = _translationProvider.GetTranslation(translationToken);

                string errorMessage = translationItem.NoTextAvailable() ?
                    (translationToken.ResourceKey + ": {0}").WithTokens(metadata.DisplayName) :
                    translationItem.ToString(metadata.DisplayName);

                var clientRules = GetClientRules(metadata, context, attr, errorMessage);
                validators.Add(new MyValidator(attr, errorMessage, metadata, context, clientRules));
            }

            return validators;
        }

        /// <summary>
        /// Get client rules
        /// </summary>
        /// <param name="metadata">Model meta data</param>
        /// <param name="context">Controller context</param>
        /// <param name="attr">Attribute being localized</param>
        /// <param name="formattedError">Localized error message</param>
        /// <returns>Collection (may be empty) with error messages for client side</returns>
        protected virtual IEnumerable<ModelClientValidationRule> GetClientRules(ModelMetadata metadata,
                                                                                ControllerContext context,
                                                                                ValidationAttribute attr,
                                                                                string formattedError)
        {
            var clientValidable = attr as IClientValidatable;
            var clientRules = clientValidable == null ?
                _clientValidationRuleFactory.GetClientValidationRules(attr, formattedError) :
                clientValidable.GetClientValidationRules(metadata, context);

            foreach (var clientRule in clientRules)
            {
                clientRule.ErrorMessage = formattedError;
            }

            return clientRules;
        }

        #region Nested type: MyValidator

        private class MyValidator : ModelValidator
        {
            private readonly ValidationAttribute _attribute;
            private readonly IEnumerable<ModelClientValidationRule> _clientRules;
            private readonly string _errorMsg;

            public MyValidator(ValidationAttribute attribute, string errorMsg, ModelMetadata metadata,
                               ControllerContext controllerContext, IEnumerable<ModelClientValidationRule> clientRules)
                : base(metadata, controllerContext)
            {
                _attribute = attribute;
                _errorMsg = errorMsg;
                _clientRules = clientRules;
            }

            public override bool IsRequired
            {
                get { return _attribute is RequiredAttribute; }
            }

            public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
            {
                return _clientRules;
            }

            public override IEnumerable<ModelValidationResult> Validate(object container)
            {
                var context = new ValidationContext(container, null, null);
                var result = _attribute.GetValidationResult(Metadata.Model, context);
                if (result == null)
                    yield break;

                string errorMsg;
                lock (_attribute)
                {
                    string defaultErrorMessage = _attribute.ErrorMessage.IsNullOrEmpty() ? "#&#" : _attribute.ErrorMessage;
                    _attribute.ErrorMessage = _errorMsg;
                    errorMsg = _attribute.FormatErrorMessage(Metadata.GetDisplayName());
                    _attribute.ErrorMessage = defaultErrorMessage;
                }

                if (result.MemberNames == null || !result.MemberNames.Any())
                {
                    yield return new ModelValidationResult { Message = errorMsg };
                }
                else
                {
                    foreach (string name in result.MemberNames)
                    {
                        yield return new ModelValidationResult { MemberName = name, Message = errorMsg };
                    }
                }
            }
        }

        #endregion
    }
}
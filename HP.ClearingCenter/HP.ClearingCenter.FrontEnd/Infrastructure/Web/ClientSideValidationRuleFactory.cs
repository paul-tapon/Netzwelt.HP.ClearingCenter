using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public class ClientSideValidationRuleFactory
    {
        static ClientSideValidationRuleFactory()
        {
            SetupValidatorRules();
        }

        private static void SetupValidatorRules()
        {
            AddRule<RequiredAttribute>((attr, errMsg) => new[] {
                new ModelClientValidationRequiredRule(errMsg)
            });

            AddRule<StringLengthAttribute>((attr, errMsg) => new[] {
                new ModelClientValidationStringLengthRule(errMsg, attr.MinimumLength, attr.MaximumLength)
            });

            AddRule<RangeAttribute>((attr, errMsg) => new[] {
                new ModelClientValidationRangeRule(errMsg, attr.Minimum, attr.Maximum)
            });

            AddRule<RegularExpressionAttribute>((attr, errMsg) => new[] {
                new ModelClientValidationRegexRule(errMsg, attr.Pattern)
            });

            AddRule<CompareAttribute>((attr, errMsg) => new[] {
                new ModelClientValidationEqualToRule(errMsg, CompareAttribute.FormatPropertyForClientValidation(attr.OtherProperty))
            });

            AddRule<EmailRequiredAttribute>((attr, errMsg) => new[] {
                new ModelClientValidationRegexRule(errMsg, attr.Pattern)
            });

            //TODO: Add support for other validation attributes here.
        }

        private static void AddRule<T>(Func<T, string, IEnumerable<ModelClientValidationRule>> ruleFactory) where T : ValidationAttribute
        {
            Func<ValidationAttribute, string, IEnumerable<ModelClientValidationRule>> factory =
                (attr, err) => ruleFactory((T)attr, err);

            _validatorRules.Add(typeof(T), factory);
        }

        private static IDictionary<Type, Func<ValidationAttribute, string, IEnumerable<ModelClientValidationRule>>> _validatorRules =
            new Dictionary<Type, Func<ValidationAttribute, string, IEnumerable<ModelClientValidationRule>>>();

        public virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules(ValidationAttribute attr, string errorMessage)
        {
            Type validatorType = attr.GetType();
            if (!_validatorRules.ContainsKey(validatorType)) return Enumerable.Empty<ModelClientValidationRule>();

            var factory = _validatorRules[validatorType];
            return factory(attr, errorMessage);
        }

    }
}
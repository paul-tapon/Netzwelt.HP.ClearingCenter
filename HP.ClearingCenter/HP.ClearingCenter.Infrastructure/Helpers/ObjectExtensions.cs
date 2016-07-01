using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HP.ClearingCenter.Infrastructure.Helpers
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object instance)
        {
            return instance == null;
        }

        public static bool Exists(this object instance)
        {
            return !instance.IsNull();
        }

        public static void ShouldNotBeNull(this object instance, string argName = "Argument")
        {
            Protect.AgainstNullArgument(instance, argName);
        }

        public static IDictionary<string, string> ToKeyValues(this object instance)
        {
            if (instance == null)
            {
                return new Dictionary<string, string>();
            }

            var properties = TypeDescriptor.GetProperties(instance);

            var hash = new Dictionary<string, string>(properties.Count);

            foreach (PropertyDescriptor descriptor in properties)
            {
                var key = descriptor.Name;
                var value = descriptor.GetValue(instance);

                if (value != null)
                {
                    hash.Add(key, value.ToString());
                }
                else
                {
                    hash.Add(key, string.Empty);
                }
            }

            return hash;
        }

        public static string ToJson(this object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }

        public static T DeserializeFromJsonAs<T>(this string json) where T : class
        {
            var instance = DeserializeFromJsonAs(json, typeof(T));
            return (T)instance;
        }

        public static object DeserializeFromJsonAs(this string json, Type type)
        {
            Type jsonConvertType = typeof(JsonConvert);
            var deserializeMethod = jsonConvertType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(x => x.Name == "DeserializeObject" && x.IsGenericMethod)
                .MakeGenericMethod(type);

            return deserializeMethod.Invoke(null, new[] { json });
        }

        public static IEnumerable<ValidationResult> InvokeValidationAttributes(this object instance)
        {
            // get all properties decorated with a [ValidationAttribute]
            var properties = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property => property.GetCustomAttributes(typeof(ValidationAttribute), true).Any());

            // get all DataAnnotation attributes for the retrieved property
            foreach (var propertyInfo in properties)
            {
                var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);

                foreach (ValidationAttribute item in validationAttributes)
                {
                    ValidationException error = null;
                    var propertyValue = propertyInfo.GetValue(instance, null);

                    try
                    {
                        var validator = new ValidationContext(instance, null, null)
                        {
                            MemberName = propertyInfo.Name,
                            DisplayName = propertyInfo.Name
                        };

                        item.Validate(propertyValue, validator);
                    }
                    catch (ValidationException ex)
                    {
                        error = ex;
                    }

                    if (!error.IsNull()) yield return error.ValidationResult;
                }
            }

            yield break;
        }
    }
}

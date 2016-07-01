using System;
using System.Web.Mvc;
using FluentValidation;

namespace HP.ClearingCenter.Web.Core.Helpers
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type type)
        {
           return DependencyResolver.Current.GetService(typeof(IValidator<>).MakeGenericType(type)) as IValidator;
        }

        public IValidator<T> GetValidator<T>()
        {
           return DependencyResolver.Current.GetService<IValidator<T>>();
        }
    }
}

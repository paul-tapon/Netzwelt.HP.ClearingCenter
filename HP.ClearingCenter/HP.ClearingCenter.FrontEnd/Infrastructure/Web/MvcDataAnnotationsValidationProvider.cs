using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public class MvcDataAnnotationsValidationProvider : IValidationProvider
    {
        public MvcDataAnnotationsValidationProvider(ModelStateDictionary modelState)
        {
            _modelState = modelState;
            _errors = new List<ValidationResult>();
            InitializeFrom(_modelState);
        }

        public bool IsValid()
        {
            return _modelState.IsValid;
        }

        public void ReportError(string errorMessage)
        {
            ValidationResult error = null;
            error = new ValidationResult(errorMessage);

            _errors.Add(error);
            _modelState.AddModelError(string.Empty, error.ErrorMessage);
        }

        public IEnumerable<ValidationResult> GetErrors()
        {
            return _errors;
        }

        public IEnumerable<ValidationResult> Validate(object instance)
        {
            return instance
                .InvokeValidationAttributes()
                .ToList();
        }

        private void InitializeFrom(ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return;

            foreach (ModelState item in modelState.Values)
            {
                foreach (ModelError error in item.Errors)
                {
                    _errors.Add(new ValidationResult(error.ErrorMessage));
                }
            }
        }

        private readonly ModelStateDictionary _modelState;
        private readonly List<ValidationResult> _errors;
    }
}
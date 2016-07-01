using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
    public class NullValidationProvider : IValidationProvider
    {
        private IList<ValidationResult> errors = new List<ValidationResult>();

        public bool IsValid()
        {
            return !this.errors.Any();
        }

        public void ReportError(string errorMessage)
        {
            this.errors.Add(new ValidationResult(errorMessage));
        }

        public IEnumerable<ValidationResult> GetErrors()
        {
            return this.errors;
        }

        public IEnumerable<ValidationResult> Validate(object instance)
        {
            return instance
                .InvokeValidationAttributes()
                .ToList();
        }
    }
}
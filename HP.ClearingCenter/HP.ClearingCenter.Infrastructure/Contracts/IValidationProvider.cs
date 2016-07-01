using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IValidationProvider
    {
        bool IsValid();
        void ReportError(string errorMessage);
        IEnumerable<ValidationResult> GetErrors();
        IEnumerable<ValidationResult> Validate(object instance);
    }
}

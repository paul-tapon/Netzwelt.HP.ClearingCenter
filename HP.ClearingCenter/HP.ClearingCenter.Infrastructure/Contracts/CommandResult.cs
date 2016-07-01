using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public class CommandResult
    {
        static CommandResult()
        {
            Successful = new CommandResult(true);
        }

        public static CommandResult Successful { get; set; }

        public CommandResult(object response)
        {
            Errors = Enumerable.Empty<ValidationResult>();
            _response = response;
        }

        public CommandResult(object response, IEnumerable<ValidationResult> errors)
        {
            _response = response;
            Errors = errors;
        }

        public CommandResult(IEnumerable<ValidationResult> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationResult> Errors { get; private set; }

        public virtual bool IsSuccessful()
        {
            return !Errors.Any();
        }

        public virtual T Response<T>()
        {
            return (T)_response;
        }

        private object _response;
    }
}

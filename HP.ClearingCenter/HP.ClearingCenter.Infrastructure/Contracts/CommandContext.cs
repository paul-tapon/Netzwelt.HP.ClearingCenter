using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ClearingCenter.Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface ICommandContext<T> where T : class, ICommand
    {
        T Args { get; }
        void SetResponse(object response);
        void ReportError(string errorMessage);
    }

    public class CommandContext<T> : ICommandContext<T> where T : class, ICommand
    {
        public CommandContext(ICommandHandler<T> processor, IValidationProvider validationProvider)
        {
            _processor = processor;
            _validationProvider = validationProvider;
        }

        public virtual T Args
        {
            get { return _commandArgs; }
        }

        public virtual void SetResponse(object response)
        {
            _response = response;
        }

        public virtual void ReportError(string errorMessage)
        {
            errorMessage.ShouldNotBeEmpty("errorMessage");
            _validationProvider.ReportError(errorMessage);
        }

        public virtual CommandResult Execute(T command)
        {
            _commandArgs = command;

            // if there are errors, don't execute.
            if (!_validationProvider.IsValid())
            {
                return FailedResult(_validationProvider);
            }

            // execute the command processor.
            _processor.Execute(this);

            // generate CommandResult
            return _validationProvider.IsValid() ? new CommandResult(_response) : FailedResult(_validationProvider);
        }

        private CommandResult FailedResult(IValidationProvider commandState)
        {
            IEnumerable<ValidationResult> errors = commandState.GetErrors();
            return new CommandResult(_response, errors);
        }

        private readonly ICommandHandler<T> _processor;
        private readonly IValidationProvider _validationProvider;
        private T _commandArgs;
        private object _response;
    }
}

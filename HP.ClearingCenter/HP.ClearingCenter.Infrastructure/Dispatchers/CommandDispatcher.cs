using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using Ninject;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HP.ClearingCenter.Infrastructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel _kernel;

        public CommandDispatcher(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            _kernel = kernel;
        }

        public virtual IValidationProvider ValidationProvider { get; set; }

        public CommandResult Dispatch<TParameter>(TParameter command) where TParameter : class, ICommand
        {
            var handler = _kernel.Get<ICommandHandler<TParameter>>();
            var context = new CommandContext<TParameter>(handler, this.ValidationProvider ?? DefaultValidationProvider.Instance);

            return context.Execute(command);
        }

        class DefaultValidationProvider : IValidationProvider
        {
            public readonly static IValidationProvider Instance = new DefaultValidationProvider();

            private DefaultValidationProvider() {}

            public bool IsValid()
            {
                return true;
            }

            public void ReportError(string errorMessage) {}

            public IEnumerable<ValidationResult> GetErrors()
            {
                yield break;
            }

            public IEnumerable<ValidationResult> Validate(object instance)
            {
                return instance
                    .InvokeValidationAttributes()
                    .ToList();
            }
        }
    }
}

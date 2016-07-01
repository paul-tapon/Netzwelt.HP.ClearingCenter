using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IApplicationContext
    {
        ITranslationProvider TranslationProvider { get; }
        
        IRequestLocaleProvider LocaleProvider { get; }

        IConfigurationProvider Configuration { get; }
        
        IQueryDispatcher Query { get; }

        ICommandDispatcher Command { get; }

        IDataContextFactory DataContextFactory { get; }

        IDebugSettingsProvider DebugSettingsProvider { get; }

        IAuthenticationProvider AuthenticationProvider { get; }

        IAuthorizationProvider AuthorizationProvider { get; }

        TResult PerformQuery<TResult>(IQuery<TResult> args);

        CommandResult ExecuteCommand<TCommand>(TCommand args, IValidationProvider validationProvider = null) where TCommand : class, ICommand;

        DateTime GetUtcNow();

        IUserInfo GetCurrentUser();
    }
}

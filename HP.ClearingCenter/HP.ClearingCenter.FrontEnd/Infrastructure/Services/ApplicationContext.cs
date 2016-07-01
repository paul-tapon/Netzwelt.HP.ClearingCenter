using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Dispatchers;
using HP.ClearingCenter.Infrastructure.Helpers;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
    public class ApplicationContext : IApplicationContext
    {
        private IKernel kernel;

        public ApplicationContext(IKernel kernel)
        {
            this.kernel = kernel;
            this.TranslationProvider = this.kernel.Get<ITranslationProvider>();
            this.LocaleProvider = this.kernel.Get<IRequestLocaleProvider>();
            this.Configuration = this.kernel.Get<IConfigurationProvider>();
            this.Command = this.kernel.Get<ICommandDispatcher>();
            this.Query = this.kernel.Get<IQueryDispatcher>();
            this.DebugSettingsProvider = this.kernel.Get<IDebugSettingsProvider>();
            this.AuthenticationProvider = this.kernel.Get<IAuthenticationProvider>();
            this.AuthorizationProvider = this.kernel.Get<IAuthorizationProvider>();
        }

        public ITranslationProvider TranslationProvider { get; private set; }

        public IRequestLocaleProvider LocaleProvider { get; private set; }

        public IConfigurationProvider Configuration { get; private set; }

        public virtual IQueryDispatcher Query { get; private set; }

        public virtual ICommandDispatcher Command { get; private set; }

        public IDebugSettingsProvider DebugSettingsProvider { get; private set; }

        public IAuthenticationProvider AuthenticationProvider { get; private set; }

        public IDataContextFactory DataContextFactory { get { throw new NotImplementedException(); } }

        public IAuthorizationProvider AuthorizationProvider { get; private set; }

        public TResult PerformQuery<TResult>(IQuery<TResult> args)
        {
            return this.Query.Dispatch<TResult>(args);
        }

        public CommandResult ExecuteCommand<TCommand>(TCommand args, IValidationProvider validationProvider = null) where TCommand : class, ICommand
        {
            if (this.Command is CommandDispatcher)
            {
                (this.Command as CommandDispatcher).ValidationProvider = validationProvider ?? new NullValidationProvider();
            }
            
            return this.Command.Dispatch<TCommand>(args);
        }

        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }

        public IUserInfo GetCurrentUser()
        {
            return this.AuthenticationProvider.GetCurrentUser() ?? new DummyUser();
        }

        class DummyUser : IUserInfo
        {
            public string Username
            {
                get { return "system"; }
            }

            public string Nickname 
            {
                get { return "system"; }
            }
            
            public string LanguageIsoCode
            {
                get { return "en"; }
            }

            public bool IsAuthenticated()
            {
                return true;
            }
        }
    }
}
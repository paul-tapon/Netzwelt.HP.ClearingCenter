using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Security.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Security.CommandHandlers
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private IApplicationContext app;

        public SignInCommandHandler(IApplicationContext app)
        {
            this.app = app;
        }

        public void Execute(ICommandContext<SignInCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                this.ExecuteCommand(context, db);
            }
        }

        private void ExecuteCommand(ICommandContext<SignInCommand> context, ClearingCenterDataContext db)
        {
            //TODO: call B2i security adapter
            
            string username = (context.Args.Username ?? string.Empty).Trim().ToUpperInvariant();
            var user = db.ApplicationUsers.FirstOrDefault(x => x.Username.ToUpper() == username);
            if (user.IsNull() || !user.Password.IsRawInputValid(context.Args.Password))
            {
                context.ReportError("Invalid username or password.");
                return;
            }

            var userInfo = new UserInfo
            {
                Username = user.Username,
                Nickname = user.Username,
                LanguageIsoCode = this.app.LocaleProvider.GetCurrentLocale().LanguageIsoCode
            };
            
            app.AuthenticationProvider.PersistAuthenticationToken(userInfo, context.Args.RememberMe);
        }

        class UserInfo : IUserInfo
        {

            public string Username { get; set; }

            public string Nickname { get; set; }

            public string LanguageIsoCode { get; set; }

            public bool IsAuthenticated()
            {
                return this.Username.IsNullOrEmpty();
            }
        }
    }
}

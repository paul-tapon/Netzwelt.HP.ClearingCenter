using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Security.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Security.CommandHandlers
{
    public class AuthenticateApiRequestCommandHandler : ICommandHandler<AuthenticateApiRequestCommand>
    {
        public void Execute(ICommandContext<AuthenticateApiRequestCommand> context)
        {
            using (var db = new ClearingCenterDataContext()) 
            {
                var user = db.ApplicationUsers.FirstOrDefault(x =>
                    x.Username == context.Args.Username &&
                    x.IsApiAccessEnabled);

                if (user == null) 
                {
                    context.ReportError("Unauthorized API user.");
                    return;
                }

                if (user.ApiKey != context.Args.ApiKey)
                {
                    context.ReportError("Unauthorized API credentials.");
                }
            }
        }
    }
}

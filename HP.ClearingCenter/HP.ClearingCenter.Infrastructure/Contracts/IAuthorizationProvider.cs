using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IAuthorizationProvider
    {
        bool IsAuthorized(string username, string securePath);
    }
}

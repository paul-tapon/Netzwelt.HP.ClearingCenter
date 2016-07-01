using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Security.Services
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        public AuthorizationProvider() { }

        public bool IsAuthorized(string username, string securePath)
        {
            using (var db = new ClearingCenterDataContext())
            {
                if (string.IsNullOrEmpty(securePath)) return false;
                
                string usr = username.Trim().ToUpperInvariant();
                var user = db.ApplicationUsers.FirstOrDefault(x => x.Username.ToUpper() == usr);
                if (user == null) return false;

                var accessRightsToken = (SecureAccessRights)Enum.Parse(typeof(SecureAccessRights), securePath);
                return EvaluateAccessRights(user, accessRightsToken);
            }
        }

        private bool EvaluateAccessRights(ApplicationUser user, SecureAccessRights securePath)
        {
            if (user.IsAdmin) return true;

            switch (securePath)
            {
                case SecureAccessRights.ClearingProcess:
                    return user.IsClearingEnabled;
                case SecureAccessRights.ReceivingProcess:
                    return user.IsReceivingEnabled;
                case SecureAccessRights.ProductManagement:
                    return user.IsProductManagementEnabled;
                default:
                    return false;
            }
        }

        
    }
}

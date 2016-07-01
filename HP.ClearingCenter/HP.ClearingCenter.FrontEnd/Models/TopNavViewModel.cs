using HP.ClearingCenter.Application.Security.Services;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models
{
    public class TopNavViewModel
    {   
        private IApplicationContext app;
        private IUserInfo user;
        
        public TopNavViewModel(IApplicationContext app)
        {
            this.app = app;
            this.user = app.GetCurrentUser();
        }

        public bool IsReceivingEnabled
        {
            get
            {
                return this.IsAccessGranted(SecureAccessRights.ReceivingProcess);
            }
        }

        public bool IsClearingEnabled
        {
            get
            {
                return this.IsAccessGranted(SecureAccessRights.ClearingProcess);
            }
        }

        public bool IsProductManagementEnabled
        {
            get
            {
                return this.IsAccessGranted(SecureAccessRights.ProductManagement);
            }
        }

        private bool IsAccessGranted(SecureAccessRights accessRights)
        {
            return this.app.AuthorizationProvider.IsAuthorized(this.user.Username, accessRights.ToString());
        }
    }
}
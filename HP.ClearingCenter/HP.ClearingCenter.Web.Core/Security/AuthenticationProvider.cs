using System;
using System.Web;
using System.Web.Security;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using HP.ClearingCenter.Web.Core.Helpers;

namespace HP.ClearingCenter.Web.Core.Security
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly HttpContextBase _httpContext;

        public AuthenticationProvider(HttpContextBase httpContextFactory)
        {
            _httpContext = httpContextFactory;
        }

        public IUserInfo GetCurrentUser()
        {
            var cookie = _httpContext.Request.Cookies.GetCookie(FormsAuthentication.FormsCookieName);
            if (cookie.IsNull()) return WebUserInfo.Unauthenticated;

            FormsAuthenticationTicket authToken = FormsAuthentication.Decrypt(cookie.Value);
            WebUserInfo userInfo = authToken.UserData.DeserializeFromJsonAs<WebUserInfo>();

            return userInfo;
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public void PersistAuthenticationToken(IUserInfo userInfo, bool isPersistent)
        {
            DateTime authDateUtc = DateTime.UtcNow;
            DateTime authExpiry = authDateUtc.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);

            var authToken = new FormsAuthenticationTicket(1,
                userInfo.Username,
                authDateUtc,
                authExpiry,
                isPersistent,
                userInfo.ToJson());

            string encTicket = FormsAuthentication.Encrypt(authToken);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            _httpContext.Response.Cookies.Set(authCookie);
        }
    }
}

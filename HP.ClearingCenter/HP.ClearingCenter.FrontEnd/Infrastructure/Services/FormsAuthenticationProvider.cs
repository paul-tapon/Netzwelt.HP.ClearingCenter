using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Services
{
    public class FormsAuthenticationProvider : IAuthenticationProvider
    {
        private Func<HttpContextBase> httpContextFactory;
        private HttpContextBase httpContext;

        public FormsAuthenticationProvider(Func<HttpContextBase> httpContextFactory)
        {
            this.httpContextFactory = httpContextFactory;
        }

        public HttpContextBase HttpContext
        {
            get
            {
                if (this.httpContext == null)
                {
                    this.httpContext = httpContextFactory();
                }

                return this.httpContext;
            }
        }

        public IUserInfo GetCurrentUser()
        {
            var cookie = this.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie.IsNull()) return WebUserInfo.Unauthenticated;

            FormsAuthenticationTicket authToken = FormsAuthentication.Decrypt(cookie.Value);
            WebUserInfo userInfo = authToken.UserData.DeserializeFromJsonAs<WebUserInfo>();

            return userInfo;
        }

        public void SignOut()
        {
            this.HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();
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
            this.HttpContext.Response.Cookies.Set(authCookie);
        }

        class WebUserInfo : IUserInfo
        {
            static WebUserInfo()
            {
                Unauthenticated = new WebUserInfo();
            }

            public static IUserInfo Unauthenticated { get; private set; }

            public string Username { get; set; }

            public string Nickname { get; set; }
            
            public string LanguageIsoCode { get; set; }

            public virtual bool IsAuthenticated()
            {
                return !Username.IsNullOrEmpty();
            }
        }
    }
}
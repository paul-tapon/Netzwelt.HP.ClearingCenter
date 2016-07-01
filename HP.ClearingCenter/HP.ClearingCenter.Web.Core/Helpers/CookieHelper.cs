using System;
using System.Collections.Generic;
using System.Web;
using HP.ClearingCenter.Infrastructure.Helpers;

namespace HP.ClearingCenter.Web.Core.Helpers
{
    public static class CookieHelper
    {
        public static HttpCookie GetCookie(this HttpCookieCollection cookies, string key)
        {
            for (int cookieIndex = 0; cookieIndex < cookies.AllKeys.Length; cookieIndex++)
            {
                var cookie = cookies[cookieIndex];
                if (cookie.Name == key)
                {
                    if (cookie.Value.IsNullOrEmpty() || cookie.Value.Trim().IsNullOrEmpty()) continue;
                    return cookie;
                }
            }

            // cookie not found
            return null;
        }

        public static void ExpireCookie(this HttpCookie cookie)
        {
            cookie.Value = string.Empty;
            cookie.Expires = DateTime.UtcNow.AddDays(-99);
        }

        public static void ExpireCookie(this HttpCookieCollection cookies, string key)
        {
            for (int cookieIndex = 0; cookieIndex < cookies.AllKeys.Length; cookieIndex++)
            {
                var cookie = cookies[cookieIndex];
                if (cookie.Name != key) continue;
                ExpireCookie(cookie);
            }
        }

        public static void ClearCookie(this HttpCookieCollection cookies, string key)
        {
            var cookiesForRemoval = new List<HttpCookie>();

            for (int cookieIndex = 0; cookieIndex < cookies.AllKeys.Length; cookieIndex++)
            {
                var cookie = cookies[cookieIndex];
                if (cookie.Name != key) continue; // ignore this cookie. we're only interested on cookie with name == key

                if (cookie.Value.IsNullOrEmpty())
                { // expire the empty cookie
                    ExpireCookie(cookie);
                }
                else
                {
                    cookiesForRemoval.Add(cookie);
                }

            }

            cookiesForRemoval.ForEach(x => cookies.Remove(x.Name));
            cookiesForRemoval.Clear();
            cookiesForRemoval = null;
        }
    }
}

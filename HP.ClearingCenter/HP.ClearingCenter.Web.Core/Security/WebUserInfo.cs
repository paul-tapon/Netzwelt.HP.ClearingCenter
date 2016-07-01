using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;

namespace HP.ClearingCenter.Web.Core.Security
{
    public class WebUserInfo : IUserInfo
    {
        static WebUserInfo()
        {
            Unauthenticated = new WebUserInfo();
        }

        public static IUserInfo Unauthenticated { get; private set; }

        public string Username { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string LanguageCode { get; set; }

        public virtual bool IsAuthenticated()
        {
            return !Username.IsNullOrEmpty();
        }
    }
}

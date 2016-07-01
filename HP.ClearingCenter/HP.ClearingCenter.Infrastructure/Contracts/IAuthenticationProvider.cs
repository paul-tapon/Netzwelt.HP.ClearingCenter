namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IAuthenticationProvider
    {
        IUserInfo GetCurrentUser();
        void SignOut();
        void PersistAuthenticationToken(IUserInfo userInfo, bool isPersistent);
    }
}

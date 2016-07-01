namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IUserInfo
    {
        string Username { get; }
        string Nickname { get; }
        string LanguageIsoCode { get; }
        bool IsAuthenticated();
    }
}

namespace InStock.Backend.AccountService.Abstraction.Services
{
    public enum UserClaim
    {
        Session_Read,
        Account_Create
    }

    public interface IIdentityService
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken);
        Task<string> VerifyUserCredentialsAsync(string username, string password, IList<string> claims);
    }
}
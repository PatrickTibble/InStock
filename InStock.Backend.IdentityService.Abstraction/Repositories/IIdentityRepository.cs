using InStock.Backend.AccountService.Abstraction.Services;

namespace InStock.Backend.AccountService.Data.IdentityAccessManagement
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken);
        Task<string> VerifyUserCredentialsAsync(string username, string password, IList<string> claims);
    }
}
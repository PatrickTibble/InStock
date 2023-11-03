using InStock.Backend.IdentityService.Abstraction.Services;

namespace InStock.Backend.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken);
        Task<string?> VerifyUserCredentialsAsync(string username, string password, IList<string> claims);
    }
}
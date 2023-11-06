using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Register;

namespace InStock.Common.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken);
        Task<string?> GetUsernameAsync(string accessToken);
        Task<bool> RegisterUserAsync(string username, string password);
        Task<string?> VerifyUserCredentialsAsync(string username, string password, IList<string> claims);
    }
}
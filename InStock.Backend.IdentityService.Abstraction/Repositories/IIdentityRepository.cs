using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;

namespace InStock.Backend.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken);
        Task<string?> GetUsernameAsync(string accessToken);
        Task<bool> RegisterUserAsync(string username, string password);
        Task<string?> VerifyUserCredentialsAsync(string username, string password, IList<string> claims);
    }
}
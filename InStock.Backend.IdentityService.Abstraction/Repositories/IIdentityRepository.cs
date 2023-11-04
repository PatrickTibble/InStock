using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;

namespace InStock.Backend.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken, CancellationToken? token = null);
        Task<bool> RegisterUserAsync(string username, string password, CancellationToken? token);
        Task<string?> VerifyUserCredentialsAsync(string username, string password, IList<string> claims, CancellationToken? token = null);
    }
}
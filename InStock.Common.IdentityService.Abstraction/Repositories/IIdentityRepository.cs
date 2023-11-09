using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;

namespace InStock.Common.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<Token?> GetIdTokenAsync(string accessToken);
        Task<bool> SaveTokenPairAsync(string accessToken, int idTokenId, string refreshToken);
        Task<bool> StoreTokensAsync(string idToken, AccessRefreshTokenPair tokenPair);
        Task<bool> ValidateTokenAsync(string token);
        Task<bool> ValidateTokenPairAsync(AccessRefreshTokenPair request);
    }
}
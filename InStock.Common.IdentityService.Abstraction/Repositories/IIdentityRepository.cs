using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;

namespace InStock.Common.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<string?> GetIdTokenAsync(string username);
        Task<bool> SaveTokenPairAsync(string accessToken, string claims, DateTime atExpiry, int idTokenId, string refreshToken, DateTime rtExpiry);
        Task<bool> StoreTokensAsync(string idToken, AccessRefreshTokenPair tokenPair);
        Task<bool> ValidateTokenAsync(string token);
        Task<bool> ValidateTokenPairAsync(AccessRefreshTokenPair request);
    }
}
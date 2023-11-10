using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public Task<StoredAccessToken?> GetAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<StoredToken?> GetIdentityTokenAsync(string idToken)
        {
            throw new NotImplementedException();
        }

        public Task<StoredToken?> GetIdentityTokenAsync(int identityTokenId)
        {
            throw new NotImplementedException();
        }

        public Task<StoredRefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateTokenFamilyAsync(StoredRefreshToken storedRefreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InvalidateTokensAsync(StoredRefreshToken storedRefreshToken, StoredAccessToken storedAccessToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveTokensAsync(string idToken, string newAccessToken, string newRefreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
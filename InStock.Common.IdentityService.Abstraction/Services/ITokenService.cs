using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface ITokenService
    {
        string? CreateIdToken(string username, AccessRefreshTokenPair token);
        AccessRefreshTokenPair? CreateAccessRefreshTokenPair(UserToken userToken);
        UserToken ReadToken(string token);
        AccessRefreshTokenPair? RefreshWithTokenPair(AccessRefreshTokenPair request);
        bool ValidateToken(string token);
    }
}
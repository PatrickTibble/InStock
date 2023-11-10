using InStock.Common.IdentityService.Abstraction.Entities;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Create a token from the user token.
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        Task<string?> CreateTokenAsync(UserToken userToken);

        /// <summary>
        /// Creates an Access Token with default expiration and claims.
        /// </summary>
        /// <returns></returns>
        Task<string?> CreateAccessTokenAsync();

        /// <summary>
        /// Creates a Refresh Token with default expiration.
        /// </summary>
        /// <returns></returns>
        Task<string?> CreateRefreshTokenAsync();

        /// <summary>
        /// Creates an Identity Token with username as the subject.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<string?> CreateIdentityTokenAsync(string username);

        /// <summary>
        /// Read a user token from a token.
        /// </summary>
        Task<UserToken?> ReadTokenAsync(string token);
    }
}
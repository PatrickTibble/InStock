using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.InvalidateToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface IIdentityService
    {
        /// <summary>
        /// Retrieve access, id, and refresh tokens from the Identity Service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post($"/{Constants.GetToken}")]
        Task<Result<GetTokenResponse>> GetTokenAsync([Body] GetTokenRequest request);

        /// <summary>
        /// Validates the access, id, or refresh token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post($"/{Constants.ValidateToken}")]
        Task<Result<ValidateTokenResponse>> ValidateTokenAsync([Body] ValidateTokenRequest request);

        /// <summary>
        /// Refreshes the access and refresh tokens.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post($"/{Constants.RefreshToken}")]
        Task<Result<AccessRefreshTokenPair>> RefreshTokenAsync([Body] AccessRefreshTokenPair request);

        /// <summary>
        /// Invalidates token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post($"/{Constants.InvalidateToken}")]
        Task<Result<InvalidateTokenResponse>> InvalidateTokenAsync(InvalidateTokenRequest request);
    }
}
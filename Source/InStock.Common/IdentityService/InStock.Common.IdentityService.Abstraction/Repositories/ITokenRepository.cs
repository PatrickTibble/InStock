﻿using InStock.Common.IdentityService.Abstraction.Entities;

namespace InStock.Common.IdentityService.Abstraction.Repositories
{
    public interface ITokenRepository
    {
        /// <summary>
        /// Retrieves an IdentityToken from storage based on its token value.
        /// </summary>
        /// <param name="idToken"></param>
        /// <returns></returns>
        Task<StoredToken?> GetIdentityTokenAsync(string idToken);

        /// <summary>
        /// Retrieves an IdentityToken from storage based on its token id.
        /// </summary>
        /// <param name="identityTokenId"></param>
        /// <returns></returns>
        Task<StoredToken?> GetIdentityTokenAsync(int identityTokenId);

        /// <summary>
        /// Retrieves an AccessToken from storage based on its token value.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<StoredAccessToken?> GetAccessTokenAsync(string accessToken);

        /// <summary>
        /// Retrieves a RefreshToken from storage based on its token value.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<StoredRefreshToken?> GetRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Invalidates all tokens associated with the given identity or refresh token.
        /// </summary>
        /// <param name="storedToken"></param>
        /// <returns></returns>
        Task InvalidateTokenFamilyAsync(StoredToken storedToken);

        /// <summary>
        /// Saves a set of tokens to storage.
        /// </summary>
        /// <param name="idToken"></param>
        /// <param name="newAccessToken"></param>
        /// <param name="newRefreshToken"></param>
        /// <returns></returns>
        Task SaveTokensAsync(string idToken, string newAccessToken, string newRefreshToken);

        /// <summary>
        /// Invalidates a pair of Access and Refresh tokens.
        /// </summary>
        /// <param name="storedRefreshToken"></param>
        /// <param name="storedAccessToken"></param>
        /// <returns></returns>
        Task<bool> InvalidateTokensAsync(StoredRefreshToken storedRefreshToken, StoredAccessToken storedAccessToken);

        /// <summary>
        /// Validates a token in storage.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ValidateTokenAsync(string token);
    }    
}
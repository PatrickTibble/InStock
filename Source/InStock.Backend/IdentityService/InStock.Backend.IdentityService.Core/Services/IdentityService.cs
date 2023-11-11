using InStock.Common.Abstraction.Logger;
using InStock.Common.IdentityService.Abstraction.Exceptions;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger _logger;
        private readonly ITokenService _tokenService;

        public IdentityService(
            ITokenService tokenService,
            ITokenRepository tokenRepository,
            ILogger logger)
        {
            _tokenRepository = tokenRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<Result<GetTokenResponse>> GetTokenAsync([Body] GetTokenRequest request)
        {
            try
            {
                var accessTokenTask = _tokenService.CreateAccessTokenAsync();
                var refreshTokenTask = _tokenService.CreateRefreshTokenAsync();
                var idTokenTask = _tokenService.CreateIdentityTokenAsync(request.Username);

                await Task
                    .WhenAll(accessTokenTask, refreshTokenTask, idTokenTask)
                    .ConfigureAwait(false);

                var accessToken = accessTokenTask.Result;
                var refreshToken = refreshTokenTask.Result;
                var idToken = idTokenTask.Result;

                if (string.IsNullOrWhiteSpace(refreshToken) 
                    || string.IsNullOrWhiteSpace(accessToken)
                    || string.IsNullOrWhiteSpace(idToken))
                {
                    // unable to create tokens
                    throw new CreateTokenException("Unable to Create Tokens");
                }

                await _tokenRepository
                    .SaveTokensAsync(idToken, accessToken, refreshToken)
                    .ConfigureAwait(false);

                return new Result<GetTokenResponse>(new GetTokenResponse
                {
                    AccessToken = accessToken,
                    IdentityToken = idToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);

                if (ex is CreateTokenException)
                {
                    return BadRequest<GetTokenResponse>(ex.Message);
                }
            }

            return InternalServerError<GetTokenResponse>("Unable to create token");
        }

        public async Task<Result<AccessRefreshTokenPair>> RefreshTokenAsync([Body] AccessRefreshTokenPair request)
        {
            try
            {
                var accessTokenTask = _tokenService.ReadTokenAsync(request.AccessToken);
                var refreshTokenTask = _tokenService.ReadTokenAsync(request.RefreshToken);

                var storedRefreshTokenTask = _tokenRepository.GetRefreshTokenAsync(request.RefreshToken);
                var storedAccessTokenTask = _tokenRepository.GetAccessTokenAsync(request.AccessToken);

                await Task
                    .WhenAll(accessTokenTask, refreshTokenTask, storedRefreshTokenTask, storedAccessTokenTask)
                    .ConfigureAwait(false);

                var accessToken = accessTokenTask.Result;
                var refreshToken = refreshTokenTask.Result;

                if (accessToken == default || refreshToken == default)
                {
                    // invalid tokens. We didn't make these
                    throw new CreateTokenException("Invalid Tokens. Tokens not created by IDS.");
                }

                var storedRefreshToken = storedRefreshTokenTask.Result;
                var storedAccessToken = storedAccessTokenTask.Result;

                if (storedRefreshToken == default || storedAccessToken == default)
                {
                    // invalid tokens. we haven't saved these in the db. 
                    throw new CreateTokenException("Invalid Tokens. Tokens do not exist in Token Repo.");
                }

                var idToken = await _tokenRepository
                    .GetIdentityTokenAsync(storedAccessToken.IdentityTokenId)
                    .ConfigureAwait(false);

                if (idToken == default
                    || idToken.Invalidated
                    || storedRefreshToken.Invalidated
                    || storedAccessToken.Invalidated)
                {
                    // We've previously invalidated these.
                    // Ensure the whole family is invalidated
                    await _tokenRepository
                        .InvalidateTokenFamilyAsync(storedRefreshToken)
                        .ConfigureAwait(false);

                    throw new CreateTokenException("Invalid Tokens. Attempted to refresh invalidated tokens.");
                }

                var newAccessTokenTask = _tokenService.CreateAccessTokenAsync();
                var newRefreshTokenTask = _tokenService.CreateRefreshTokenAsync();

                await Task
                    .WhenAll(newAccessTokenTask, newRefreshTokenTask)
                    .ConfigureAwait(false);

                var newAccessToken = newAccessTokenTask.Result;
                var newRefreshToken = newRefreshTokenTask.Result;

                await _tokenRepository
                    .SaveTokensAsync(idToken.TokenValue, newAccessToken!, newRefreshToken!)
                    .ConfigureAwait(false);

                _ = await _tokenRepository
                    .InvalidateTokensAsync(storedRefreshToken, storedAccessToken)
                    .ConfigureAwait(false);

                return new Result<AccessRefreshTokenPair>(new AccessRefreshTokenPair
                {
                    AccessToken = newAccessToken!,
                    RefreshToken = newRefreshToken!
                });
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);

                if (ex is CreateTokenException)
                {
                    return BadRequest<AccessRefreshTokenPair>(ex.Message);
                }
            }

            return InternalServerError<AccessRefreshTokenPair>("Unable to refresh token");
        }

        public async Task<Result<ValidateTokenResponse>> ValidateTokenAsync([Body] ValidateTokenRequest request)
        {
            try
            {
                // confirm that we made the token.
                var readTask = _tokenService.ReadTokenAsync(request.Token);

                // confirm that we know we made it (it's in the db)
                var retrievalTask = _tokenRepository.ValidateTokenAsync(request.Token);

                await Task
                    .WhenAll(readTask, retrievalTask)
                    .ConfigureAwait(false);

                return new Result<ValidateTokenResponse>(new ValidateTokenResponse
                {
                    IsValid = readTask.Result != default && retrievalTask.Result
                });
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);

                if (ex is CreateTokenException)
                {
                    return BadRequest<ValidateTokenResponse>(ex.Message);
                }
            }

            return InternalServerError<ValidateTokenResponse>("Unable to validate token");
        }

        private static Result<T> BadRequest<T>(string message)
            where T : class
            => new(400, message);

        private static Result<T> InternalServerError<T>(string message)
            where T : class
            => new(500, message);
    }
}
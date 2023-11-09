using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken;
using InStock.Common.Models.Base;
using Refit;
using ILogger = InStock.Common.Abstraction.Logger.ILogger;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly ILogger _logger;
        private readonly ITokenService _tokenService;

        public IdentityService(
            IIdentityRepository identityRepository,
            ITokenService tokenService,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<Result<GetTokenResponse>> GetTokenAsync([Body] GetTokenRequest request)
        {
            try
            {
                var badRequestResponse = new Result<GetTokenResponse>(400, "Unable to create token");

                var tokenPair = _tokenService.CreateAccessRefreshTokenPair(new UserToken
                {
                    Expiry = request.Expiry,
                    Claims = request.Claims
                });

                if (tokenPair == default)
                {
                    return badRequestResponse;
                }

                var idToken = (await _identityRepository.GetIdTokenAsync(request.Username))?.TokenValue;

                if (string.IsNullOrWhiteSpace(idToken))
                {
                    idToken = _tokenService.CreateIdToken(new UserToken
                    {
                        Expiry = DateTime.UtcNow.AddMonths(1),
                        Claims = new Dictionary<string, string>
                        {
                            { "username", request.Username }
                        }
                    });
                }

                if (string.IsNullOrWhiteSpace(idToken))
                {
                    return badRequestResponse;
                }

                var saveResult = await _identityRepository
                    .StoreTokensAsync(idToken, tokenPair)
                    .ConfigureAwait(false);

                if (!saveResult)
                {
                    return badRequestResponse;
                }

                return new Result<GetTokenResponse>(new GetTokenResponse
                {
                    AccessToken = tokenPair.AccessToken,
                    IdentityToken = idToken,
                    RefreshToken = tokenPair.RefreshToken
                });
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }
            
            return new Result<GetTokenResponse>(500, "Unable to create token");
        }

        public async Task<Result<AccessRefreshTokenPair>> RefreshTokenAsync([Body] AccessRefreshTokenPair request)
        {
            try
            {
                var badRequest = BadRequest<AccessRefreshTokenPair>("Unable to refresh token");

                var validationResult = await _identityRepository
                    .ValidateTokenPairAsync(request)
                    .ConfigureAwait(false);

                if (!validationResult)
                {
                    return badRequest;
                }

                var idToken = await _identityRepository
                    .GetIdTokenAsync(request.AccessToken)
                    .ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(idToken?.TokenValue))
                {
                    return badRequest;
                }

                var refreshResult = _tokenService.RefreshWithTokenPair(request);
                
                if (refreshResult == default)
                {
                    return badRequest;
                }

                var saveResult = await _identityRepository
                    .SaveTokenPairAsync(request.AccessToken, idToken.Id, request.RefreshToken)
                    .ConfigureAwait(false);

                if (!saveResult)
                {
                    return badRequest;
                }

                return new Result<AccessRefreshTokenPair>(refreshResult);
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return InternalServerError<AccessRefreshTokenPair>("Unable to refresh token");
        }

        public async Task<Result<ValidateTokenResponse>> ValidateTokenAsync([Body] ValidateTokenRequest request)
        {
            try
            {
                // confirm that we made the token.
                var validationResult = _tokenService.ValidateToken(request.Token);

                // confirm that we know we made it
                validationResult &= await _identityRepository
                    .ValidateTokenAsync(request.Token)
                    .ConfigureAwait(false);

                return new Result<ValidateTokenResponse>(new ValidateTokenResponse
                {
                    IsValid = validationResult
                });
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return InternalServerError<ValidateTokenResponse>("Unable to validate token");
        }

        private Result<T> BadRequest<T>(string message)
            where T : class
            => new Result<T>(400, message);

        private Result<T> InternalServerError<T>(string message)
            where T : class
            => new Result<T>(500, message);
    }
}
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims;
using InStock.Common.Models.Base;
using ILogger = InStock.Common.Abstraction.Logger.ILogger;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(
            IIdentityRepository identityRepository,
            IUserRepository userRepository,
            ITokenService tokenService,
            IHashService hashService,
            ILogger logger)
        {
            _identityRepository = identityRepository;
            _tokenService = tokenService;
            _hashService = hashService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<UserClaimsResponse>> GetUserClaimsAsync(UserClaimsRequest request)
        {
            try
            {
                var userToken = _tokenService
                    .ReadToken(request.AccessToken!);

                if (userToken == default)
                {
                    return new Result<UserClaimsResponse>(401, "Access token is invalid");
                }

                var response = new UserClaimsResponse
                {
                    Username = userToken.Username,
                    Claims = userToken.Claims
                };

                return new Result<UserClaimsResponse>(response!);
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return new Result<UserClaimsResponse>(500, "Internal Server Error");
        }

        public async Task<Result<RegistrationResponse>> RegisterUserAsync(RegistrationRequest request)
        {
            try
            {
                _hashService.CreateHash(request.Password!, out var passwordHash, out var passwordSalt);
                // get rid of their password as quickly as possible.
                request.Password = null;

                var isUsernameAvailable = await _userRepository
                    .GetUsernameAvailableAsync(request.Username)
                    .ConfigureAwait(false);

                if (!isUsernameAvailable)
                {
                    return new Result<RegistrationResponse>(400, "Username is already taken");
                }

                var userId = await _userRepository
                    .CreateUserAsync(request.Username!, request.FirstName!, request.LastName!, passwordHash, passwordSalt)
                    .ConfigureAwait(false);

                if (userId <= 0)
                {
                    return new Result<RegistrationResponse>(400, "Failed to create user");
                }

                var response = new RegistrationResponse
                {
                    IsRegistered = true
                };

                return new Result<RegistrationResponse>(response);
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return new Result<RegistrationResponse>(500, "Internal Server Error");
        }

        public async Task<Result<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            try
            {
                var result = await _identityRepository
                    .VerifyUserCredentialsAsync(request.Username!, request.Password!, request.Claims!)
                    .ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(result))
                {
                    return new Result<AuthenticationResponse>(401, "Authentication Error");
                }

                var response = new AuthenticationResponse
                {
                    AccessToken = result
                };

                return new Result<AuthenticationResponse>(response);
            }
            catch (Exception e)
            {
                await _logger
                    .LogExceptionAsync(e)
                    .ConfigureAwait(false);
            }
            return new Result<AuthenticationResponse>(500, "Internal Server Error");
        }
    }
}
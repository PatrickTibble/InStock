using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.AccountService.Abstraction.Repositories;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.Core.Extensions;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.InvalidateToken;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Backend.AccountService.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private readonly IHashService _hashService;
        private readonly IIdentityService _identityService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(
            IHashService hasService,
            IIdentityService identityService,
            IAccountRepository accountRepository,
            ILogger logger)
        {
            _logger = logger;
            _hashService = hasService;
            _identityService = identityService;
            _accountRepository = accountRepository;
        }

        public async Task<Result<LoginResponse>> CreateAccountAsync(CreateAccountRequest request)
        {
            try
            {
                _hashService.CreateHash(request.Password!, out var hash, out var salt);
                request.Password = null;

                var user = await _accountRepository
                    .GetUserByUsernameAsync(request.Username)
                    .ConfigureAwait(false);

                if (user != default)
                {
                    return new Result<LoginResponse>(400, "Username already exists.");
                }

                var addUserTask = _accountRepository.AddUserAsync(
                    request.FirstName,
                    request.LastName,
                    request.Username,
                    hash,
                    salt);

                var getTokenTask = GetUserTokensAsync(request);

                await Task.WhenAll(addUserTask, getTokenTask)
                    .ConfigureAwait(false);

                if (getTokenTask.Result.StatusCode == 200)
                {
                    return getTokenTask.Result;
                }

                return new Result<LoginResponse>(201, "Account was created successfully. You may now log in.");
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return new Result<LoginResponse>(500, "Unable to create account.");
        }

        public async Task<Result<LoginResponse>> LoginAsync([Body] LoginRequest request)
        {
            try
            {
                var credentialsResult = await VerifyUserCredentials(request)
                    .ConfigureAwait(false);

                if (credentialsResult != null)
                {
                    return credentialsResult;
                }

                var tokenResult = await GetUserTokensAsync(request)
                    .ConfigureAwait(false);

                return tokenResult;
            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return new Result<LoginResponse>(500, "Unable to login at this time.");
        }

        private async Task<Result<LoginResponse>?> VerifyUserCredentials(LoginRequest request)
        {
            var hashedUser = await _accountRepository
                .GetHashedUserByUsernameAsync(request.Username)
                .ConfigureAwait(false);

            if (hashedUser == default)
            {
                request.Password = null;
                return new Result<LoginResponse>(401, "Invalid username or password.");
            }

            var loginResult = _hashService.VerifyHash(request.Password!, hashedUser.PasswordHash!, hashedUser.PasswordSalt!);
            request.Password = null;

            if (!loginResult)
            {
                return new Result<LoginResponse>(401, "Invalid username or password.");
            }

            return null;
        }

        private async Task<Result<LoginResponse>> GetUserTokensAsync(LoginRequest request)
        {
            var tokenPair = await _identityService.
                    GetTokenAsync(new GetTokenRequest
                    {
                        Username = request.Username!
                    })
                    .ConfigureAwait(false);

            if (tokenPair.StatusCode != 200 || tokenPair.Data == null)
            {
                return new Result<LoginResponse>(tokenPair.StatusCode, "Unable to login at this time.");
            }

            var currentTokensForClient = await _accountRepository
                .GetUserTokensForClientAsync(request.ClientId!)
                .ConfigureAwait(false);

            await _accountRepository
                .AddUserTokenForClientAsync(
                    request.Username!,
                    request.ClientId!,
                    request.ClientName!,
                    request.ClientDescription!,
                    tokenPair.Data.IdentityToken!)
                .ConfigureAwait(false);

            _identityService
                .InvalidateTokenAsync(new InvalidateTokenRequest
                {
                    IdentityTokens = currentTokensForClient
                })
                .FireAndForgetSafeAsync();

            return new Result<LoginResponse>(new LoginResponse
            {
                AccessToken = tokenPair.Data.AccessToken,
                RefreshToken = tokenPair.Data.RefreshToken
            });
        }
    }
}
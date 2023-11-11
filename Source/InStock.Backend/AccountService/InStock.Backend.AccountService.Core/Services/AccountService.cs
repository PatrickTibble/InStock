using InStock.Common.Abstraction.Logger;
using InStock.Common.AccountService.Abstraction.Repositories;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Backend.AccountService.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHashService _hashService;
        private readonly ILogger _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IIdentityService _identityService;

        public AccountService(
            IHashService hasService,
            IIdentityService identityService,
            IAccountRepository accountRepository,
            ILogger logger)
        {
            _hashService = hasService;
            _logger = logger;
            _accountRepository = accountRepository;
            _identityService = identityService;
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

                var getTokenTask = _identityService.GetTokenAsync(new GetTokenRequest
                {
                    Username = request.Username!
                });

                await Task
                    .WhenAll(addUserTask, getTokenTask)
                    .ConfigureAwait(false);

                var tokenPair = getTokenTask.Result;

                if (tokenPair.StatusCode == 200 && tokenPair.Data != null)
                {
                    return new Result<LoginResponse>(new LoginResponse
                    {
                        AccessToken = tokenPair.Data?.AccessToken,
                        RefreshToken = tokenPair.Data?.RefreshToken
                    });
                }

                return new Result<LoginResponse>(201, "Account was created successfully. You may now log in.");
            }
            catch (Exception ex)
            {
                // log it
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
                _hashService.CreateHash(request.Password!, out var hash, out var salt);
                request.Password = null;

                var hashedUser = await _accountRepository
                    .GetHashedUserByUsernameAsync(request.Username)
                    .ConfigureAwait(false);

                if (hashedUser == default)
                {
                    return new Result<LoginResponse>(401, "Invalid username or password.");
                }

                var loginResult = _hashService.VerifyHash(request.Password!, hashedUser.PasswordHash!, hashedUser.PasswordSalt!);

                if (!loginResult)
                {
                    return new Result<LoginResponse>(401, "Invalid username or password.");
                }

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

                return new Result<LoginResponse>(new LoginResponse
                {
                    AccessToken = tokenPair.Data.AccessToken,
                    RefreshToken = tokenPair.Data.RefreshToken
                });

            }
            catch (Exception ex)
            {
                await _logger
                    .LogExceptionAsync(ex)
                    .ConfigureAwait(false);
            }

            return new Result<LoginResponse>(500, "Unable to login at this time.");
        }
    }
}
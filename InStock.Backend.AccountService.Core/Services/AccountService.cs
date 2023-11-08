using InStock.Common.Abstraction.Logger;
using InStock.Common.AccountService.Abstraction.Entities;
using InStock.Common.AccountService.Abstraction.Repositories;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.AccountService.Abstraction.TransferObjects.SessionState;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Backend.AccountService.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(
            IAccountRepository accountRepository,
            IUserService userService,
            IIdentityService identityService,
            ILogger logger)
        {
            _logger = logger;
            _identityService = identityService;
            _userService = userService;
            _accountRepository = accountRepository;
        }

        public async Task<Result<CreateAccountResponse>> CreateAccountAsync(CreateAccountRequest request)
        {
            try
            {
                var user = await _accountRepository
                    .GetUserByUsernameAsync(request.Username)
                    .ConfigureAwait(false);

                if (user == null)
                {
                    // try to register the user with Identity Server
                    var registrationResult = await _userService
                        .RegisterUserAsync(new RegistrationRequest
                        {
                            Username = request.Username,
                            Password = request.Password
                        })
                        .ConfigureAwait(false);

                    if (registrationResult?.StatusCode != 200)
                    {
                        return new Result<CreateAccountResponse>(registrationResult?.StatusCode ?? 400, "Unable to create account.");
                    }

                    var response = registrationResult.Data;

                    if ((response?.IsRegistered ?? false))
                    {
                        // Try to create the account
                        user = new UserAccount
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Username = request.Username
                        };

                        var result = await _accountRepository
                            .AddUserAsync(user)
                            .ConfigureAwait(false);

                        return new Result<CreateAccountResponse>(
                            new CreateAccountResponse
                            {
                                Success = result
                            });
                    }
                }

                return new Result<CreateAccountResponse>(400, "User already exists.");
            }
            catch (Exception ex)
            {
                // log it
                await _logger.LogExceptionAsync(ex);
            }

            return new Result<CreateAccountResponse>(500, "Unable to create account.");
        }

        public async Task<Result<SessionStateResponse>> GetSessionStateAsync(string? accessToken)
        {
            try
            {
                // verify access token
                var claimsResult = await _identityService.GetUserClaimsAsync(new UserClaimsRequest
                {
                    AccessToken = accessToken
                });

                if (claimsResult?.StatusCode != 200)
                {
                    return new Result<SessionStateResponse>(claimsResult?.StatusCode ?? 401, "Invalid access token.");
                }

                var claimsResponse = claimsResult.Data;

                if (!(claimsResponse?.Claims?.Any(c => c.Equals(UserClaim.Session_Read.ToClaimType())) ?? false)
                    || string.IsNullOrWhiteSpace(claimsResponse?.Username))
                {
                    return new Result<SessionStateResponse>(401, "Invalid access token.");
                }

                return new Result<SessionStateResponse>(
                    new SessionStateResponse
                    {
                        IsCurrentSessionActive = true
                    });
            }
            catch (Exception ex)
            {
                // log it
                await _logger.LogExceptionAsync(ex);
            }

            return new Result<SessionStateResponse>(500, "Unable to get session state.");
        }

        public async Task<Result<LoginResponse>> LoginAsync([Body] LoginRequest request)
        {
            try
            {
                var result = await _identityService.AuthenticateAsync(new AuthenticationRequest
                {
                    Username = request.Username,
                    Password = request.Password,
                    Claims = new List<string>
                    {
                        UserClaim.Session_Read.ToClaimType()
                    }
                });

                if (result.StatusCode != 200 || result.Data == null)
                {
                    return new Result<LoginResponse>(result.StatusCode, "Invalid username or password.");
                }

                var response = result.Data;

                if (string.IsNullOrWhiteSpace(response.AccessToken))
                {
                    return new Result<LoginResponse>(401, "Invalid username or password.");
                }

                return new Result<LoginResponse>(
                    new LoginResponse
                    {
                        AccessToken = response.AccessToken
                    });
            }
            catch (Exception ex)
            {
                // log it
                await _logger.LogExceptionAsync(ex);
            }

            return new Result<LoginResponse>(500, "Unable to login at this time.");
        }
    }
}
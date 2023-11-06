using InStock.Backend.AccountService.Abstraction.Entities;
using InStock.Backend.AccountService.Abstraction.Repositories;
using InStock.Backend.AccountService.Abstraction.Services;
using InStock.Backend.AccountService.Abstraction.TransferObjects.Base;
using InStock.Backend.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Backend.AccountService.Abstraction.TransferObjects.Login;
using InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState;
using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.Extensions;
using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using Refit;

namespace InStock.Backend.AccountService.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IIdentityService _identityService;
        private readonly IAccountRepository _accountRepository;

        private IList<UserSession> _sessions = new List<UserSession>();

        public AccountService(
            IAccountRepository accountRepository,
            IIdentityService identityService)
        {
            _identityService = identityService;
            _accountRepository = accountRepository;
        }

        public async Task<Result<CreateAccountResponse>> CreateAccountAsync(CreateAccountRequest request)
        {
            try
            {
                var user = await _accountRepository.GetUserByUsernameAsync(request.Username);

                if (user == null)
                {
                    // Try to create the account
                    user = new UserAccount
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Username = request.Username
                    };

                    var result = await _accountRepository.AddUserAsync(user);
                    return new Result<CreateAccountResponse>(
                        new CreateAccountResponse
                        {
                            Success = result
                        });
                }
                return new Result<CreateAccountResponse>(400, "User already exists.");
            }
            catch (Exception)
            {
                // log it
            }

            return new Result<CreateAccountResponse>(500, "Unable to create account.");
        }

        public async Task<Result<SessionStateResponse>> GetSessionStateAsync(string? accessToken)
        {
            try
            {
                // verify access token
                var claimsResponse = await _identityService.GetUserClaimsAsync(new IdentityService.Abstraction.TransferObjects.UserClaims.UserClaimsRequest
                {
                    AccessToken = accessToken
                });

                if (claimsResponse == null
                    || !(claimsResponse.Claims?.Any(c => c.Equals(UserClaim.Session_Read.ToClaimType())) ?? false)
                    || string.IsNullOrWhiteSpace(claimsResponse.Username))
                {
                    return new Result<SessionStateResponse>(401, "Invalid access token.");
                }

                var session = _sessions.FirstOrDefault(s => s.Username!.Equals(claimsResponse.Username));

                if (session == null)
                {
                    return new Result<SessionStateResponse>(404, "Session not found.");
                }

                session.AccessToken = accessToken!;

                return new Result<SessionStateResponse>(
                    new SessionStateResponse
                    {
                        IsCurrentSessionActive = true
                    });
            }
            catch (Exception)
            {
                // log it
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

                if (result == null || string.IsNullOrWhiteSpace(result.AccessToken))
                {
                    return new Result<LoginResponse>(401, "Invalid username or password.");
                }

                _sessions.Add(new UserSession
                {
                    Username = request.Username!,
                    SessionId = Guid.NewGuid(),
                    AccessToken = result.AccessToken
                });

                return new Result<LoginResponse>(
                    new LoginResponse
                    {
                        AccessToken = result.AccessToken
                    });
            }
            catch (Exception)
            {
                // log it
            }

            return new Result<LoginResponse>(500, "Unable to login at this time."); 
        }
    }
}
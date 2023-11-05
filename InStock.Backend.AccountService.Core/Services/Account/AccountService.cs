using InStock.Backend.AccountService.Abstraction.Entities;
using InStock.Backend.AccountService.Abstraction.Repositories;
using InStock.Backend.AccountService.Abstraction.Services;

namespace InStock.Backend.AccountService.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Common.Models.Account.CreateAccount.Response> CreateAccountAsync(Common.Models.Account.CreateAccount.Request request)
        {
            var response = new Common.Models.Account.CreateAccount.Response();

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
                response.IsSuccessfulStatusCode = result;

            }

            return response;
        }

        public Task<Common.Models.Account.SessionStatus.Response> GetSessionStateAsync(string accessToken)
        {
            // retrieve user information using access token

            // retrieve session information using user info

            // return session info as response
            return Task.FromResult(new Common.Models.Account.SessionStatus.Response
            {
                CurrentSessionId = Guid.NewGuid(),
                IsCurrentSessionActive = true,
            });
        }
    }
}
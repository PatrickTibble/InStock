using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.API.Account;

namespace InStock.Frontend.Core.Repositories
{
    public class AccountRepository : IAccountRepository
	{
        private readonly IAccountService _accountService;
        private readonly CancellationToken _token;

        public AccountRepository(IAccountService accountService, ITaskCancellationService taskCancellationService)
        {
            _accountService = accountService;
            _token = taskCancellationService.GetToken();
        }

        public async Task<LoginResult> LoginAsync(string? username, string? password)
        {
            var loginRequest = new API.Models.Account.Login.Request()
            {
                Username = username,
                Password = password
            };

            if (!loginRequest.IsValid)
            {
                return LoginResult.Default;
            }

            var response = await _accountService.LoginAsync(loginRequest, _token).ConfigureAwait(false);

            return new LoginResult
            {
                IsSuccessful = response.IsSuccessfulStatusCode
            };
        }
    }
}
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Threading;

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

        public async Task<CreateAccountResult> CreateAccountAsync(string? firstName, string? lastName, string? username, string? password)
        {
            var request = new CreateAccountRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            var response = await _accountService.CreateAccountAsync(request).ConfigureAwait(false);

            return new CreateAccountResult
            {

            };
        }

        public async Task<LoginResult> LoginAsync(string? username, string? password)
        {
            var loginRequest = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            var response = await _accountService.LoginAsync(loginRequest).ConfigureAwait(false);

            return new LoginResult
            {

            };
        }
    }
}
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Settings;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.Core.Extensions;

namespace InStock.Frontend.Core.Repositories
{
    public class AccountRepository : IAccountRepository
	{
        private readonly IAccountService _accountService;
        private readonly CancellationToken _token;
        private readonly ISettingsService _settingsService;

        public AccountRepository(
            IAccountService accountService,
            ISettingsService settingsService,
            ITaskCancellationService taskCancellationService)
        {
            _accountService = accountService;
            _token = taskCancellationService.GetToken();
            _settingsService = settingsService;
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

            var result = await _accountService
                .CreateAccountAsync(request)
                .ConfigureAwait(false);

            if (result.IsSuccessfulStatusCode()
                && result.Data != null)
            {
                // TODO: Save tokens
                return new CreateAccountResult
                {
                    AccountCreationSuccessful = true
                };
            }

            return new CreateAccountResult
            {
                ErrorMessage = result.Error
            };
        }

        public async Task<LoginResult> LoginAsync(string? username, string? password)
        {
            var loginRequest = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            var result = await _accountService
                .LoginAsync(loginRequest)
                .ConfigureAwait(false);

            if (result.IsSuccessfulStatusCode())
            {
                return new LoginResult
                {
                    AccessToken = result.Data?.AccessToken
                };
            }

            return new LoginResult
            {
                ErrorMessage = result.Error
            };
        }
    }
}
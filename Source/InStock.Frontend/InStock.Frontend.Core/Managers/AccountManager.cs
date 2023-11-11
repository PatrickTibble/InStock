using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Core.Extensions;

namespace InStock.Frontend.Core.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountService _accountService;
        private readonly ISettingsManager _settingsManager;

        public AccountManager(
            IAccountService accountService,
            ISettingsManager settingsManager)
        {
            _accountService = accountService;
            _settingsManager = settingsManager;
        }

        public async Task<BooleanResult> CreateAccountAsync(string? firstName, string? lastName, string? username, string? password)
        {
            var deviceId = await _settingsManager
                .GetDeviceIdAsync()
                .ConfigureAwait(false);

            var request = new CreateAccountRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password,
                ClientId = deviceId
            };

            var result = await _accountService
                .CreateAccountAsync(request)
                .ConfigureAwait(false);

            if (!result.IsSuccessfulStatusCode())
            {
                return result.ToBooleanResult();
            }

            if (result.Data != null)
            {
                var accessTokenTask = _settingsManager.GetAccessTokenAsync();
                var refreshTokenTask = _settingsManager.GetRefreshTokenAsync();

                await Task
                    .WhenAll(accessTokenTask, refreshTokenTask)
                    .ConfigureAwait(false);
            }

            return result.ToBooleanResult();
        }

        public async Task<BooleanResult> LoginAsync(string? username, string? password)
        {
            var deviceId = await _settingsManager
                .GetDeviceIdAsync()
                .ConfigureAwait(false);

            var loginRequest = new LoginRequest()
            {
                Username = username,
                Password = password,
                ClientId = deviceId
            };

            var result = await _accountService
                .LoginAsync(loginRequest)
                .ConfigureAwait(false);

            if (result.IsSuccessfulStatusCode() && result.Data != null)
            {
                var accessTokenTask = _settingsManager.SetAccessTokenAsync(result.Data.AccessToken);
                var refreshTokenTask = _settingsManager.SetRefreshTokenAsync(result.Data.RefreshToken);

                await Task
                    .WhenAll(accessTokenTask, refreshTokenTask)
                    .ConfigureAwait(false);
            }

            return result.ToBooleanResult();
        }
    }
}
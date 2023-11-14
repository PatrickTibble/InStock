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
            try
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
                    ClientId = deviceId.Value
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
                    await SaveTokens(result.Data)
                        .ConfigureAwait(false);
                }

                return result.ToBooleanResult();
            }
            catch (Exception ex)
            {
                return new BooleanResult
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
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
                ClientId = deviceId.Value
            };

            var result = await _accountService
                .LoginAsync(loginRequest)
                .ConfigureAwait(false);

            if (result.IsSuccessfulStatusCode() && result.Data != null)
            {
                await SaveTokens(result.Data)
                    .ConfigureAwait(false);
            }

            return result.ToBooleanResult();
        }

        private Task SaveTokens(LoginResponse response)
        {
            var accessTokenTask = _settingsManager.SetAccessTokenAsync(response.AccessToken);
            var refreshTokenTask = _settingsManager.SetRefreshTokenAsync(response.RefreshToken);

            return Task.WhenAll(accessTokenTask, refreshTokenTask);
        }
    }
}
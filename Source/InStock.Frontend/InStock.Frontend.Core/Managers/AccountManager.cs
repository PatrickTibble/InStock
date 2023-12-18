using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.AccountService.Abstraction.Entities.ClientInfo;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.Core.Extensions;
using InStock.Common.Models.Base;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Core.Extensions;

namespace InStock.Frontend.Core.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly ILogger _logger;
        private readonly IPlatformInfoService _platformInfoService;
        private readonly IAccountService _accountService;
        private readonly IStorageManager _settingsManager;

        public AccountManager(
            IAccountService accountService,
            IStorageManager settingsManager,
            IPlatformInfoService platformInfoService,
            ILogger logger)
        {
            _logger = logger;
            _platformInfoService = platformInfoService;
            _accountService = accountService;
            _settingsManager = settingsManager;
        }

        public async Task<BooleanResult> CreateAccountAsync(string? firstName, string? lastName, string? username, string? password)
        {
            try
            {
                var request = new CreateAccountRequest
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    Password = password
                };

                var result = await SendRequest(request, _accountService.CreateAccountAsync)
                    .ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogExceptionAsync(ex).FireAndForgetSafeAsync();
                return new BooleanResult
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<BooleanResult> LoginAsync(string? username, string? password)
        {
            try
            {
                var loginRequest = new LoginRequest()
                {
                    Username = username,
                    Password = password
                };

                var result = await SendRequest(loginRequest, _accountService.LoginAsync)
                    .ConfigureAwait(false);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
                return new BooleanResult
                {
                    Result = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<BooleanResult> SendRequest<T>(T request, Func<T, Task<Result<LoginResponse>>> method)
            where T : IClientInfoModel
        {
            try
            {
                var deviceId = await _settingsManager
                    .GetDeviceIdAsync()
                    .ConfigureAwait(false);

                request.ClientId = deviceId.Value;
                request.ClientName = _platformInfoService.ClientName;
                request.ClientDescription = _platformInfoService.ClientDescription;

                var result = await method(request)
                    .ConfigureAwait(false);

                if (result.IsSuccessfulStatusCode() && result.Data != null)
                {
                    await SaveTokens(result.Data)
                        .ConfigureAwait(false);
                }

                return result.ToBooleanResult();
            }
            catch (Exception e)
            {
                _logger.LogExceptionAsync(e).FireAndForgetSafeAsync();
                return new BooleanResult { Result = false, ErrorMessage = e.Message };
            }
        }

        private Task SaveTokens(LoginResponse response)
        {
            var accessTokenTask = _settingsManager.SetAccessTokenAsync(response.AccessToken);
            var refreshTokenTask = _settingsManager.SetRefreshTokenAsync(response.RefreshToken);

            return Task.WhenAll(accessTokenTask, refreshTokenTask);
        }
    }
}
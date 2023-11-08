using InStock.Common.Abstraction.Logger;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.AccountService.Abstraction.TransferObjects.SessionState;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Backend.AccountService.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;

        public AccountService(
            ILogger logger)
        {
            _logger = logger;
        }

        public async Task<Result<CreateAccountResponse>> CreateAccountAsync(CreateAccountRequest request)
        {
            try
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.AccountService.Abstraction.TransferObjects.SessionState;
using InStock.Common.AccountService.Abstraction.TransferObjects.Signout;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Frontend.API.Mocks
{
    public class MockAccountService : BaseMockService, IAccountService
    {
        private bool _sessionIsActive = false;

        public Task<Result<LoginResponse>> CreateAccountAsync([Body] CreateAccountRequest request)
        {
            return Delay()
                .ContinueWith(t => new Result<LoginResponse>(new LoginResponse
                {

                }));
        }

        public Task<Result<SessionStateResponse>> GetSessionStateAsync(string? accessToken)
        {
            return Delay()
                .ContinueWith(_ => new Result<SessionStateResponse>(new SessionStateResponse
                {
                    IsCurrentSessionActive = _sessionIsActive
                }));
        }

        public Task<Result<LoginResponse>> LoginAsync([Body] LoginRequest request)
        {
            _sessionIsActive = true;
            return Delay()
                .ContinueWith(_ => new Result<LoginResponse>(new LoginResponse
                {

                }));
        }

        public Task<Result<SignoutResponse>> SignoutAsync([Body] SignoutRequest request)
        {
            _sessionIsActive = false;
            return Delay()
                .ContinueWith(_ => new Result<SignoutResponse>(new SignoutResponse
                {

                }));
        }
    }
}
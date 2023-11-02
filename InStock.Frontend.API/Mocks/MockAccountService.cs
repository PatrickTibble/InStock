using InStock.Frontend.API.Account;
using Refit;

namespace InStock.Frontend.API.Mocks
{
    public class MockAccountService : BaseMockService, IAccountService
    {
        private bool _sessionIsActive = false;

        public Task<Common.Models.Account.CreateAccount.Response> CreateAccountAsync([Body] Common.Models.Account.CreateAccount.Request request, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(t => new Common.Models.Account.CreateAccount.Response
                {

                });
        }

        public Task<Common.Models.Account.SessionStatus.Response> GetSessionStatusAsync(CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Common.Models.Account.SessionStatus.Response
                {
                    IsCurrentSessionActive = _sessionIsActive,
                    CurrentSessionId = _sessionIsActive ? new Guid() : null
                });
        }

        public Task<Common.Models.Account.Login.Response> LoginAsync([Body] Common.Models.Account.Login.Request request, CancellationToken token)
        {
            _sessionIsActive = true;
            return Delay(token)
                .ContinueWith(_ => new Common.Models.Account.Login.Response
                {

                });
        }

        public Task<Common.Models.Account.Signout.Response> SignoutAsync([Body] Common.Models.Account.Signout.Request request, CancellationToken token)
        {
            _sessionIsActive = false;
            return Delay(token)
                .ContinueWith(_ => new Common.Models.Account.Signout.Response
                {

                });
        }
    }
}
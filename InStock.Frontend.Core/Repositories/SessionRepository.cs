using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.API.Account;

namespace InStock.Frontend.Core.Repositories
{
	public class SessionRepository : ISessionRepository
	{
        private readonly IAccountService _accountService;
        private readonly CancellationToken _token;

        public SessionRepository(
            IAccountService accountService,
            ITaskCancellationService taskCancellationService)
        {
            _accountService = accountService;
            _token = taskCancellationService.GetToken();
        }

        public async Task<SessionState> GetSessionStateAsync()
        {
            var response = await _accountService.GetSessionStatus(_token).ConfigureAwait(false);

            if (response.IsSuccessfulStatusCode)
            {
                return new SessionState
                {
                    IsValid = response.IsCurrentSessionActive,
                    SessionId = response.CurrentSessionId
                };
            }

            return SessionState.Default;
        }
    }
}
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Threading;

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
            var accessToken = string.Empty; // TODO: Need to get access token from login
            var response = await _accountService.GetSessionStateAsync(accessToken).ConfigureAwait(false);

            return new SessionState
            {

            };
        }
    }
}
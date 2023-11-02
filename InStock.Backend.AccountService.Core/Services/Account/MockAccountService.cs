using InStock.Backend.AccountService.Abstraction.Services;

namespace InStock.Backend.AccountService.Core.Services.Account
{
    public class MockAccountService : IAccountService
    {
        public Task<Common.Models.Account.CreateAccount.Response> CreateAccountAsync(Common.Models.Account.CreateAccount.Request request)
        {
            throw new NotImplementedException();
        }

        public Task<Common.Models.Account.SessionStatus.Response> GetSessionStateAsync(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
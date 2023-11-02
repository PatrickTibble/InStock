using InStock.Backend.AccountService.Abstraction.Services;

namespace InStock.Backend.AccountService.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        public Task<Common.Models.Account.CreateAccount.Response> CreateAccountAsync(Common.Models.Account.CreateAccount.Request request)
        {
            throw new NotImplementedException();
        }

        public Task<Common.Models.Account.SessionStatus.Response> GetSessionStateAsync(string accessToken)
        {
            // retrieve user information using access token

            // retrieve session information using user info

            // return session info as response

            throw new NotImplementedException();
        }
    }
}
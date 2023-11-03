namespace InStock.Backend.AccountService.Abstraction.Services
{
    public interface IAccountService
    {
        Task<Common.Models.Account.CreateAccount.Response> CreateAccountAsync(Common.Models.Account.CreateAccount.Request request);

        Task<Common.Models.Account.SessionStatus.Response> GetSessionStateAsync(string accessToken);
    }
}
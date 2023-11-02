using Refit;

namespace InStock.Frontend.API.Account
{
    public interface IAccountService
    {
        [Get("/api/v1/Account/SessionStatus")]
        Task<Common.Models.Account.SessionStatus.Response> GetSessionStatusAsync(CancellationToken token);

        [Post("/api/v1/Account/CreateAccount")]
        Task<Common.Models.Account.CreateAccount.Response> CreateAccountAsync([Body] Common.Models.Account.CreateAccount.Request request, CancellationToken token);

        [Post("/api/v1/Account/Login")]
        Task<Common.Models.Account.Login.Response> LoginAsync([Body] Common.Models.Account.Login.Request request, CancellationToken token);

        [Post("/api/v1/Account/Signout")]
        Task<Common.Models.Account.Signout.Response> SignoutAsync([Body] Common.Models.Account.Signout.Request request, CancellationToken token);
    }
}
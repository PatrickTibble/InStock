using Refit;

namespace InStock.Frontend.API.Account
{
    public interface IAccountService
    {
        [Get("/api/v1/Account/SessionStatus")]
        Task<Models.Account.SessionStatus.Response> GetSessionStatus(CancellationToken token);

        [Post("/api/v1/Account/Login")]
        Task<Models.Account.Login.Response> LoginAsync([Body] Models.Account.Login.Request request, CancellationToken token);

        [Post("/api/v1/Account/Signout")]
        Task<Models.Account.Signout.Response> SignoutAsync([Body] Models.Account.Signout.Request request, CancellationToken token);
    }
}
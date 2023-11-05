using InStock.Backend.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState;
using Refit;

namespace InStock.Backend.AccountService.Abstraction.Services
{
    public interface IAccountService
    {
        [Post($"/{Constants.CreateAccount}")]
        Task<CreateAccountResponse> CreateAccountAsync([Body] CreateAccountRequest request);

        [Get($"/{Constants.SessionState}")]
        Task<SessionStateResponse> GetSessionStateAsync([Header("accessToken")] string? accessToken);
    }
}
using InStock.Backend.AccountService.Abstraction.TransferObjects.Base;
using InStock.Backend.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Backend.AccountService.Abstraction.TransferObjects.Login;
using InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState;
using Refit;

namespace InStock.Backend.AccountService.Abstraction.Services
{
    public interface IAccountService
    {
        [Post($"/{Constants.CreateAccount}")]
        Task<Result<CreateAccountResponse>> CreateAccountAsync([Body] CreateAccountRequest request);

        [Get($"/{Constants.SessionState}")]
        Task<Result<SessionStateResponse>> GetSessionStateAsync([Header("accessToken")] string? accessToken);

        [Post($"/{Constants.Login}")]
        Task<Result<LoginResponse>> LoginAsync([Body] LoginRequest request);
    }
}
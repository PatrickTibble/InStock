using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.AccountService.Abstraction.Services
{
    public interface IAccountService
    {
        [Post($"/{Constants.CreateAccount}")]
        Task<Result<LoginResponse>> CreateAccountAsync([Body] CreateAccountRequest request);

        [Post($"/{Constants.Login}")]
        Task<Result<LoginResponse>> LoginAsync([Body] LoginRequest request);
    }
}
using InStock.Backend.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState;

namespace InStock.Backend.AccountService.Abstraction.Services
{
    public interface IAccountService
    {
        Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request);

        Task<SessionStateResponse> GetSessionStateAsync(string accessToken);
    }
}
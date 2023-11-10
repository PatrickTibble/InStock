using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Repositories
{
    public interface IAccountRepository
    {
        Task<CreateAccountResult> CreateAccountAsync(string? firstName, string? lastName, string? username, string? password);
        Task<LoginResult> LoginAsync(string? username, string? password);
    }
}
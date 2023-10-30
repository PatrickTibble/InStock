using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Repositories
{
    public interface IAccountRepository
    {
        Task<LoginResult> LoginAsync(string? username, string? password);
    }
}
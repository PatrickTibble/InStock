using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Managers
{
    public interface IAccountManager
    {
        Task<BooleanResult> CreateAccountAsync(string? firstName, string? lastName, string? username, string? password);

        Task<BooleanResult> LoginAsync(string? username, string? password);
    }
}
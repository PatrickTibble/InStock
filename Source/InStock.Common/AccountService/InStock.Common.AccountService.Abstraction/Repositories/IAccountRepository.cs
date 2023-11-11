using InStock.Common.AccountService.Abstraction.Entities;

namespace InStock.Common.AccountService.Abstraction.Repositories
{
    public interface IAccountRepository
    {
        Task AddUserAsync(string? firstName, string? lastName, string? username, byte[] hash, byte[] salt);
        Task<HashedUser?> GetHashedUserByUsernameAsync(string? username);
        Task<UserAccount?> GetUserByUsernameAsync(string? username);
    }
}
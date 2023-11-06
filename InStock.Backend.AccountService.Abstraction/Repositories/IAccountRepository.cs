using InStock.Common.AccountService.Abstraction.Entities;

namespace InStock.Common.AccountService.Abstraction.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> AddUserAsync(UserAccount user);
        Task<bool> DeleteUserAsync(UserAccount user);
        Task<UserAccount?> GetUserByIdAsync(int id);
        Task<UserAccount?> GetUserByUsernameAsync(string? username);
        Task<bool> UpdateUserAsync(UserAccount user);
    }
}
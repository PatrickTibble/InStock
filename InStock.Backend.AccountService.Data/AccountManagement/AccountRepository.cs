using InStock.Common.AccountService.Abstraction.Entities;
using InStock.Common.AccountService.Abstraction.Repositories;

namespace InStock.Backend.AccountService.Data.AccountManagement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IList<UserAccount> _accounts;

        public AccountRepository()
        {
            _accounts = new List<UserAccount>()
            {
                // Start with a test account
                new UserAccount
                {
                    Id = 1,
                    Username = "jsmith1",
                    FirstName = "John",
                    LastName = "Smith"
                }
            };
        }

        public Task<UserAccount?> GetUserByIdAsync(int id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == id);

            return Task.FromResult(account);
        }

        public Task<UserAccount?> GetUserByUsernameAsync(string? username)
        {
            var account = GetUser(username);

            return Task.FromResult(account);
        }

        public Task<bool> AddUserAsync(UserAccount user)
        {
            if (_accounts.Any(u => u.Id == user.Id || u.Username!.Equals(user.Username)))
            {
                // Collision
                return Task.FromResult(false);
            }

            _accounts.Add(user);

            return Task.FromResult(true);
        }

        public Task<bool> UpdateUserAsync(UserAccount user)
        {
            var userToReplace = GetUser(user.Username);
            if (userToReplace == null)
            {
                // User account not found
                return Task.FromResult(false);
            }

            var indexOfUser = _accounts.IndexOf(userToReplace);

            _accounts.Remove(userToReplace);
            _accounts.Insert(indexOfUser, user);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteUserAsync(UserAccount user)
        {
            var userToDelete = GetUser(user.Username);

            if (userToDelete == null)
            {
                // user account not found
                return Task.FromResult(false);
            }

            var removalResult = _accounts.Remove(userToDelete);

            return Task.FromResult(removalResult);
        }

        private UserAccount? GetUser(string? username) => _accounts.FirstOrDefault(u => u.Username!.Equals(username!));
    }
}
namespace InStock.Common.IdentityService.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(string username, string firstName, string lastName, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> GetUsernameAvailableAsync(string? username);
    }
}
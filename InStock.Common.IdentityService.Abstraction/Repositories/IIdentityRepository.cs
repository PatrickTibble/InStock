namespace InStock.Common.IdentityService.Abstraction.Repositories
{
    public interface IIdentityRepository
    {
        Task<bool> VerifyUserCredentialsAsync(string username, string password, IList<string> claims);
    }
}
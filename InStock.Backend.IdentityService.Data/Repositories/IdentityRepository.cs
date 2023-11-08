using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        public Task<bool> VerifyUserCredentialsAsync(string username, string password, IList<string> claims)
        {
            throw new NotImplementedException();
        }
    }
}
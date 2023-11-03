using InStock.Backend.AccountService.Abstraction.Services;
using InStock.Backend.AccountService.Data.IdentityAccessManagement;

namespace InStock.Backend.AccountService.Core.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken)
        {
            var claims = await _identityRepository.GetUserClaimsAsync(accessToken);
            return claims;
        }

        public async Task<string> VerifyUserCredentialsAsync(string username, string password, IList<string> claims)
        {
            var accessToken = await _identityRepository.VerifyUserCredentialsAsync(username, password, claims);
            return accessToken;
        }
    }
}
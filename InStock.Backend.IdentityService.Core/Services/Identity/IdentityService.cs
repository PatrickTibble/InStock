using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.Repositories;
using InStock.Backend.IdentityService.Abstraction.Services;

namespace InStock.Backend.IdentityService.Core.Services.Identity
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

        public async Task<Abstraction.TransferObjects.Register.Response> RegisterUserAsync(Abstraction.TransferObjects.Register.Request request)
        {
            var user = await _identityRepository.RegisterUserAsync(request);
        }

        public Task<Abstraction.TransferObjects.SendVerificationLink.Response> SendVerificationLinkAsync(Abstraction.TransferObjects.SendVerificationLink.Request request)
        {
            throw new NotImplementedException();
        }

        public Task<Abstraction.TransferObjects.VerifyEmail.Response> VerifyEmailAsync(Abstraction.TransferObjects.VerifyEmail.Request request)
        {
            throw new NotImplementedException();
        }

        public Task<Abstraction.TransferObjects.Authenticate.Response> VerifyUserCredentialsAsync(Abstraction.TransferObjects.Authenticate.Request request, List<string> claims)
        {
            throw new NotImplementedException();
        }
    }
}
using InStock.Common.IdentityService.Abstraction.Extensions;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHashService _hashService;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(
            IIdentityRepository identityRepository,
            IHashService hashService)
        {
            _hashService = hashService;
            _identityRepository = identityRepository;
        }

        public async Task<UserClaimsResponse> GetUserClaimsAsync(UserClaimsRequest request)
        {
            var claimsTask = _identityRepository.GetUserClaimsAsync(request.AccessToken!);
            var usernameTask = _identityRepository.GetUsernameAsync(request.AccessToken!);
            await Task.WhenAll(claimsTask, usernameTask);
            var response = new UserClaimsResponse
            {
                Username = usernameTask.Result,
                Claims = claimsTask.Result?.Select(c => c.ToClaimType()).ToList() ?? new List<string>()
            };
            return response;
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request)
        {
            var result = await _identityRepository.RegisterUserAsync(request.Username!, request.Password!);

            var response = new RegistrationResponse
            {
                IsRegistered = result
            };

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var claims = request.Claims?.ToList();
            var result = await _identityRepository.VerifyUserCredentialsAsync(request.Username!, request.Password!, claims!);

            var response = new AuthenticationResponse
            {
                AccessToken = result
            };

            return response;
        }
    }
}
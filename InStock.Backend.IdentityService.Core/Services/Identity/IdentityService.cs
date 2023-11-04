using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.Repositories;
using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.IdentityService.Core.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly ICommunicationService _communicationService;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(
            IIdentityRepository identityRepository,
            ICommunicationService communicationService)
        {
            _communicationService = communicationService;
            _identityRepository = identityRepository;
        }

        public async Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken, CancellationToken? token = null)
        {
            var claims = await _identityRepository.GetUserClaimsAsync(accessToken, token);
            return claims;
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request, CancellationToken? token = null)
        {
            var result = await _identityRepository.RegisterUserAsync(request.Username, request.Password, token);

            var response = new RegistrationResponse
            {
                IsRegistered = result
            };

            return response;
        }

        public async Task<VerificationLinkResponse> SendVerificationLinkAsync(VerificationLinkRequest request, CancellationToken? token = null)
        {
            var result = await _communicationService.SendVerificationLinkAsync(request, token);

            var response = new VerificationLinkResponse
            {
                IsSent = result
            };

            return response;
        }

        public async Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken? token = null)
        {
            var result = await _communicationService.VerifyEmailAsync(request, token);

            var response = new VerifyEmailResponse
            {
                IsVerified = result
            };

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, List<string> claims, CancellationToken? token = null)
        {
            var result = await _identityRepository.VerifyUserCredentialsAsync(request.Username!, request.Password!, claims, token);

            var response = new AuthenticationResponse
            {
                AccessToken = result
            };

            return response;
        }
    }
}
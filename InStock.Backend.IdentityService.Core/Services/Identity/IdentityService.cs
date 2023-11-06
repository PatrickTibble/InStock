using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.Extensions;
using InStock.Backend.IdentityService.Abstraction.Repositories;
using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.UserClaims;
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

        public async Task<VerificationLinkResponse> SendVerificationLinkAsync(VerificationLinkRequest request)
        {
            var result = await _communicationService.SendVerificationLinkAsync(request);

            var response = new VerificationLinkResponse
            {
                IsSent = result
            };

            return response;
        }

        public async Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest request)
        {
            var result = await _communicationService.VerifyEmailAsync(request);

            var response = new VerifyEmailResponse
            {
                IsVerified = result
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
using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;
using Refit;

namespace InStock.Backend.IdentityService.Abstraction.Services
{
    public interface IIdentityService
    {
        [Get(Constants.UserClaims)]
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken, CancellationToken? token = null);

        [Post(Constants.Authenticate)]
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, List<string> claims, CancellationToken? token = null);

        [Post(Constants.Register)]
        Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request, CancellationToken? token = null);

        [Post(Constants.SendVerificationLink)]
        Task<VerificationLinkResponse> SendVerificationLinkAsync(VerificationLinkRequest request, CancellationToken? token = null);

        [Post(Constants.VerifyEmail)]
        Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken? token = null);
    }
}
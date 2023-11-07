using InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Common.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims;
using InStock.Common.IdentityService.Abstraction.TransferObjects.VerifyEmail;
using Refit;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface IIdentityService
    {
        [Post($"/{Constants.UserClaims}")]
        Task<UserClaimsResponse> GetUserClaimsAsync([Body] UserClaimsRequest request);

        [Post($"/{Constants.Authenticate}")]
        Task<AuthenticationResponse> AuthenticateAsync([Body] AuthenticationRequest request);

        [Post($"/{Constants.Register}")]
        Task<RegistrationResponse> RegisterUserAsync([Body] RegistrationRequest request);

        [Post($"/{Constants.SendVerificationLink}")]
        Task<VerificationLinkResponse> SendVerificationLinkAsync([Body] VerificationLinkRequest request);

        [Post($"/{Constants.VerifyEmail}")]
        Task<VerifyEmailResponse> VerifyEmailAsync([Body] VerifyEmailRequest request);
    }
}
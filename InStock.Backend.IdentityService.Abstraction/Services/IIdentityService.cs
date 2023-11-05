using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.UserClaims;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace InStock.Backend.IdentityService.Abstraction.Services
{
    public interface IIdentityService
    {
        [Post($"/{Constants.UserClaims}")]
        Task<IEnumerable<string>> GetUserClaimsAsync([Body] UserClaimsRequest request);

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
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.IdentityService.Abstraction.Services
{
    public interface ICommunicationService
    {
        Task<bool> SendVerificationLinkAsync(VerificationLinkRequest request);
        Task<bool> VerifyEmailAsync(VerifyEmailRequest request);
    }
}
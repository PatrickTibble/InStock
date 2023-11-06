using InStock.Common.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Common.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface ICommunicationService
    {
        Task<bool> SendVerificationLinkAsync(VerificationLinkRequest request);
        Task<bool> VerifyEmailAsync(VerifyEmailRequest request);
    }
}
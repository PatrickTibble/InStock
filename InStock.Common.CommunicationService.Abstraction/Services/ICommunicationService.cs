using InStock.Common.CommunicationService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Common.CommunicationService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Common.CommunicationService.Abstraction.Services
{
    public interface ICommunicationService
    {
        Task<VerificationLinkResponse> SendVerificationLinkAsync(VerificationLinkRequest request);
        Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest request);
    }
}
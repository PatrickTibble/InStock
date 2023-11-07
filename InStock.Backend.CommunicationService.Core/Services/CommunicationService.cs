using InStock.Common.CommunicationService.Abstraction.Services;
using InStock.Common.CommunicationService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Common.CommunicationService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.CommunicationService.Core.Services
{
    public class CommunicationService : ICommunicationService
    {
        public Task<VerificationLinkResponse> SendVerificationLinkAsync(VerificationLinkRequest request)
        {
            return Task.FromResult<VerificationLinkResponse>(default);
        }

        public Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest request)
        {
            return Task.FromResult<VerifyEmailResponse>(default);
        }
    }
}
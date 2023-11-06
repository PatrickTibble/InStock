using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Common.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.IdentityService.Core.Services.Communication
{
    public class CommunicationService : ICommunicationService
    {
        public Task<bool> SendVerificationLinkAsync(VerificationLinkRequest request)
        {
            return Task.FromResult(true);
        }

        public Task<bool> VerifyEmailAsync(VerifyEmailRequest request)
        {
            return Task.FromResult(true);
        }
    }
}
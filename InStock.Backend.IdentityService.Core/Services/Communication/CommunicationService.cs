using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.IdentityService.Core.Services.Communication
{
    public class CommunicationService : ICommunicationService
    {
        public Task<bool> SendVerificationLinkAsync(VerificationLinkRequest request, CancellationToken? token)
        {
            return Task.FromResult(true);
        }

        public Task<bool> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken? token)
        {
            return Task.FromResult(true);
        }
    }
}
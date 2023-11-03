using InStock.Backend.IdentityService.Abstraction.Entities;
using Authenticate = InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using Register = InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using SendVerificationLink = InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using VerifyEmail = InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.IdentityService.Abstraction.Services
{
    public interface IIdentityService
    {
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken);
        Task<Authenticate.Response> VerifyUserCredentialsAsync(Authenticate.Request request, List<string> claims);
        Task<Register.Response> RegisterUserAsync(Register.Request request);
        Task<SendVerificationLink.Response> SendVerificationLinkAsync(SendVerificationLink.Request request);
        Task<VerifyEmail.Response> VerifyEmailAsync(VerifyEmail.Request request);
    }
}
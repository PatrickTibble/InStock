using InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface IIdentityService
    {
        [Post($"/{Constants.UserClaims}")]
        Task<Result<UserClaimsResponse>> GetUserClaimsAsync([Body] UserClaimsRequest request);

        [Post($"/{Constants.Authenticate}")]
        Task<Result<AuthenticationResponse>> AuthenticateAsync([Body] AuthenticationRequest request);

        [Post($"/{Constants.Register}")]
        Task<Result<RegistrationResponse>> RegisterUserAsync([Body] RegistrationRequest request);
    }
}
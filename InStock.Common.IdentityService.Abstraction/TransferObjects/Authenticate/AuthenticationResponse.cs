using InStock.Common.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationResponse : BaseResponse
    {
        public string? AccessToken { get; set; }
    }
}
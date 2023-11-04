using InStock.Backend.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationResponse : BaseResponse
    {
        public string? AccessToken { get; set; }
    }
}
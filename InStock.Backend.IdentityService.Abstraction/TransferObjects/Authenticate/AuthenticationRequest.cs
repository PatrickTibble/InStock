using InStock.Backend.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationRequest : BaseRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
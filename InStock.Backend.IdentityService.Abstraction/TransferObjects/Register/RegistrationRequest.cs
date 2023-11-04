using InStock.Backend.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.Register
{
    public class RegistrationRequest : BaseRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
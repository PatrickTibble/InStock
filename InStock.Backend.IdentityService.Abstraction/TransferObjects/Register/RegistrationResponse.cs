using InStock.Backend.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.Register
{
    public class RegistrationResponse : BaseResponse
    {
        public bool IsRegistered { get; set; }
    }
}
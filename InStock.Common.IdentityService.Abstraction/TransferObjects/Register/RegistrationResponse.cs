using InStock.Common.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.Register
{
    public class RegistrationResponse : BaseResponse
    {
        public bool IsRegistered { get; set; }
    }
}
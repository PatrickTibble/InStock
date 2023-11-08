using InStock.Common.AccountService.Abstraction.TransferObjects.Base;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Register
{
    public class RegistrationResponse : BaseResponse
    {
        public bool IsRegistered { get; set; }
    }
}
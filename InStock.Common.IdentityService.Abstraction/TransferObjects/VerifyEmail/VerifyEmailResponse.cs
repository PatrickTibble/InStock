using InStock.Common.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.VerifyEmail
{
    public class VerifyEmailResponse : BaseResponse
    {
        public bool IsVerified { get; set; }
    }
}
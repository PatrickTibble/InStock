using InStock.Backend.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink
{
    public class VerificationLinkResponse : BaseResponse
    {
        public bool IsSent { get; set; }
    }
}
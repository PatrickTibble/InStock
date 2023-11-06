using InStock.Common.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.SendVerificationLink
{
    public class VerificationLinkResponse : BaseResponse
    {
        public bool IsSent { get; set; }
    }
}
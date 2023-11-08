using System.ComponentModel.DataAnnotations;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims
{
    public class UserClaimsRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string? AccessToken { get; set; }
    }
}
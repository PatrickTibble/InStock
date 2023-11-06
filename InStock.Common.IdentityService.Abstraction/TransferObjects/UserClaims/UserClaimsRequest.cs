using System.ComponentModel.DataAnnotations;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims
{
    public class UserClaimsRequest
    {
        [Required]
        public string? AccessToken { get; set; }
    }
}
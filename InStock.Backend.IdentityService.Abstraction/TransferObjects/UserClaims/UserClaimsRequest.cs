using System.ComponentModel.DataAnnotations;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.UserClaims
{
    public class UserClaimsRequest
    {
        [Required]
        public string? AccessToken { get; set; }
    }
}
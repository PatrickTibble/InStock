using System.ComponentModel.DataAnnotations;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken
{
    public class ValidateTokenRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Token is required")]
        public string Token { get; set; } = string.Empty;
    }
}
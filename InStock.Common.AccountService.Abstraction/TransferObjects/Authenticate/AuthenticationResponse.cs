using InStock.Common.AccountService.Abstraction.TransferObjects.Base;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationResponse : BaseResponse
    {
        public string? IdToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
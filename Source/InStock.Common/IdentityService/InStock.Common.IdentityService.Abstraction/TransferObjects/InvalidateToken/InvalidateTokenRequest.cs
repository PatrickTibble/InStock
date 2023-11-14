namespace InStock.Common.IdentityService.Abstraction.TransferObjects.InvalidateToken
{
    public class InvalidateTokenRequest
    {
        public IList<string> IdentityTokens { get; set; }
    }
}
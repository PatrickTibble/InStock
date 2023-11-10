namespace InStock.Common.IdentityService.Abstraction.Entities
{
    public class StoredRefreshToken : StoredToken
    {
        public int AccessTokenId { get; set; }
    }
}
namespace InStock.Common.IdentityService.Abstraction.Entities
{
    public class StoredAccessToken : StoredToken
    {
        public int IdentityTokenId { get; set; }
    }
}
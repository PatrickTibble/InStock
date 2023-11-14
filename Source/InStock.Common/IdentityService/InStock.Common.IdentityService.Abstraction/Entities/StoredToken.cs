namespace InStock.Common.IdentityService.Abstraction.Entities
{
    public class StoredToken
    {
        public int Id { get; set; }
        public bool Invalidated { get; set; }
        public string TokenValue { get; set; } = string.Empty;
    }
}
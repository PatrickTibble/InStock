namespace InStock.Common.IdentityService.Abstraction.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string? TokenValue { get; set; }
        public bool Invalidated { get; set; }
    }
}
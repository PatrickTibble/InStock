namespace InStock.Common.AccountService.Abstraction.Entities
{
    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string? Suite { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
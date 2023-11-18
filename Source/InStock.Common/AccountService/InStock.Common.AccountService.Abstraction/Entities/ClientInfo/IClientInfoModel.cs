namespace InStock.Common.AccountService.Abstraction.Entities.ClientInfo
{
    public interface IClientInfoModel
    {
        Guid ClientId { get; set; }
        string? ClientName { get; set; }
        string? ClientDescription { get; set; }
    }
}
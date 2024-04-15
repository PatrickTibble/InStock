using InStock.Common.InventoryService.Abstraction.Entities;

namespace InStock.Frontend.Abstraction.Managers
{
    public interface IRevenueManager
    {
        Task<IList<RevenueReport>> GetRevenueReportAsync();
    }
}
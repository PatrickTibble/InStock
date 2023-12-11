using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Managers
{
    public interface IRevenueManager
    {
        Task<RevenueReport> GetRevenueReportAsync();
    }
}
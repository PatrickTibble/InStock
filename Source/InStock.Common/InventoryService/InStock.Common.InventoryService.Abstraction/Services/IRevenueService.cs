using InStock.Common.InventoryService.Abstraction.TransferObjects.Revenue.Report;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.InventoryService.Abstraction.Services
{
    public interface IRevenueService
    {
        [Get($"/{Constants.RevenueReport}")]
        Task<Result<RevenueResponse>> GetRevenueReport([Header(Constants.Authorization)] string accessToken, [Query] RevenueRequest request, CancellationToken? token = null);
    }
}
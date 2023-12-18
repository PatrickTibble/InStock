
using InStock.Common.InventoryService.Abstraction.Entities;

namespace InStock.Common.InventoryService.Abstraction.TransferObjects.Revenue.Report
{
    public class RevenueResponse
    {
        public IList<RevenueReport> Reports { get; set; }
    }
}
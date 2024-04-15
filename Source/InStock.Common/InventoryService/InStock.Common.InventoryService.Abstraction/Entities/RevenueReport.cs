using InStock.Common.Models;

namespace InStock.Common.InventoryService.Abstraction.Entities
{
    public class RevenueReport
    {
        public DateTime ReportDate { get; set; }
        public DateRange DateRange { get; set; }
        public decimal Revenue { get; set; }
    }
}

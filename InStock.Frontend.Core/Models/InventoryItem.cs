using InStock.Common.Abstraction.Repositories.Base;

namespace InStock.Frontend.Core.Models
{
    public class InventoryItem : IIdentifiable
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice { get; set; }
    }
}


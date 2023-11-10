using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Repositories
{
	public interface IInventoryRepository
	{
		public Task<IEnumerable<InventoryItem>?> GetFullInventoryAsync();
	}
}


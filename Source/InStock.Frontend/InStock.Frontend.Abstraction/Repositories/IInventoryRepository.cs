using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Repositories
{
	/// <summary>
	/// Contract for the Inventory Storage Repository.
	/// </summary>
	public interface IInventoryRepository
	{
		/// <summary>
		/// Retrieve the full inventory list.
		/// </summary>
		/// <returns></returns>
		public Task<IEnumerable<InventoryItem>?> GetFullInventoryAsync();
	}
}


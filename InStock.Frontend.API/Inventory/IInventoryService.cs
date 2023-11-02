using Refit;

namespace InStock.Frontend.API.Inventory
{
	public interface IInventoryService
	{
		[Get("/api/v1/Inventory")]
		Task<Common.Models.Inventory.GetAll.Response> GetAllAsync(CancellationToken token);

		[Get("/api/v1/Inventory/{id}")]
		Task<Common.Models.Inventory.Get.Response> GetAsync(int id, CancellationToken token);

		[Post("/api/v1/Inventory")]
		Task<Common.Models.Inventory.Insert.Response> AddOrUpdateAsync(Common.Models.Inventory.Insert.Request request, CancellationToken token);

		[Delete("/api/v1/Inventory/{id}")]
		Task<Common.Models.Inventory.Delete.Response> DeleteAsync(int id, CancellationToken token);
	}
}
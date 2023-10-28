using Refit;

namespace InStock.Frontend.API.Inventory
{
	public interface IInventoryService
	{
		[Get("/api/v1/Inventory")]
		Task<Models.Inventory.GetAll.Response> GetAllAsync(CancellationToken token);

		[Get("/api/v1/Inventory/{id}")]
		Task<Models.Inventory.Get.Response> GetAsync(int id, CancellationToken token);

		[Post("/api/v1/Inventory")]
		Task<Models.Inventory.Insert.Response> AddOrUpdateAsync(Models.Inventory.Insert.Request request, CancellationToken token);

		[Delete("/api/v1/Inventory/{id}")]
		Task<Models.Inventory.Delete.Response> DeleteAsync(int id, CancellationToken token);
	}
}


using InStock.Frontend.API.Inventory;

namespace InStock.Frontend.API.Mocks
{
    internal class MockInventoryService : BaseMockService, IInventoryService
	{
        public Task<Models.Inventory.Insert.Response> AddOrUpdateAsync(Models.Inventory.Insert.Request request, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Inventory.Insert.Response
                {

                });
        }

        public Task<Models.Inventory.Delete.Response> DeleteAsync(int id, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Inventory.Delete.Response
                {

                });
        }

        public Task<Models.Inventory.GetAll.Response> GetAllAsync(CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Inventory.GetAll.Response
                {

                });
        }

        public Task<Models.Inventory.Get.Response> GetAsync(int id, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Inventory.Get.Response
                {

                });
        }
    }
}
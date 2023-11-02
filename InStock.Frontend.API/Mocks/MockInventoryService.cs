using InStock.Frontend.API.Inventory;
using InStock.Common.Models.Mocks;

namespace InStock.Frontend.API.Mocks
{
    public class MockInventoryService : BaseMockService, IInventoryService
	{
        private IList<MockInventoryItem> _inventoryItems = new List<MockInventoryItem>()
        {
            new MockInventoryItem(0, "Test inventory item 1", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(1, "Test inventory item 2", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(2, "Test inventory item 3", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(3, "Test inventory item 4", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(4, "Test inventory item 5", "Test inventory item description. Lorem ipsum"),
            new MockInventoryItem(5, "Test inventory item 6", "Test inventory item description. Lorem ipsum")
        };

        public Task<Common.Models.Inventory.Insert.Response> AddOrUpdateAsync(Common.Models.Inventory.Insert.Request request, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Common.Models.Inventory.Insert.Response
                {

                });
        }

        public Task<Common.Models.Inventory.Delete.Response> DeleteAsync(int id, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Common.Models.Inventory.Delete.Response
                {

                });
        }

        public Task<Common.Models.Inventory.GetAll.Response> GetAllAsync(CancellationToken token)
        {
            var model = new Common.Models.Inventory.GetAll.Response
            {
                Items = new List<Common.Models.Inventory.Get.Response>(),
                IsSuccessfulStatusCode = true
            };

            foreach (var item in _inventoryItems)
            {
                model.Items.Add(item.ToResponseModel());
            }

            return Delay(token)
                .ContinueWith(_ => model);
        }

        public Task<Common.Models.Inventory.Get.Response> GetAsync(int id, CancellationToken token)
        {
            var model = new Common.Models.Inventory.Get.Response();
            var item = _inventoryItems.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                model = item.ToResponseModel();
            }

            return Delay(token)
                .ContinueWith(_ => model);
        }
    }
}
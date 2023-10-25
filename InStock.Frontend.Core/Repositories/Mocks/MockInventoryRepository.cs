using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.Repositories.Base;

namespace InStock.Frontend.Core.Repositories.Mocks
{
    public class MockInventoryRepository : Repository<InventoryItem>
	{
        public MockInventoryRepository()
        {
            //-- Seed Repo
            Add(new InventoryItem
            {
                Id = 1,
                Name = "Compact Disk Reader",
                Description = "Reads and Writes CDs",
                Cost = 12.1m,
                SalePrice = 39.99m
            });

            Add(new InventoryItem
            {
                Id = 2,
                Name = "RAM, 8 GB",
                Description = "8 GB Insertable RAM",
                Cost = 22m,
                SalePrice = 79.99m
            });

            Add(new InventoryItem
            {
                Id = 3,
                Name = "Cooling Fan",
                Description = "Simple Cooling Mechanism",
                Cost = 5m,
                SalePrice = 12.99m
            });

            Add(new InventoryItem
            {
                Id = 4,
                Name = "LED Lights - Interior",
                Description = "Visually Appealing Lights",
                Cost = 3.12m,
                SalePrice = 14.99m
            });

            Add(new InventoryItem
            {
                Id = 5,
                Name = "SSD, 128 GB",
                Description = "128 GB Solid State Drive",
                Cost = 25.93m,
                SalePrice = 49.99m
            });
        }
    }
}


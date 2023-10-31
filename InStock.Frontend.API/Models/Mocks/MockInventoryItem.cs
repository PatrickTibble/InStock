namespace InStock.Frontend.API.Models.Mocks
{
    internal class MockInventoryItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public MockInventoryItem(int id, string? name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Inventory.Get.Response ToResponseModel()
            => new Inventory.Get.Response
            {
                Id = Id,
                Name = Name,
                Description = Description,
                IsSuccessfulStatusCode = true
            };
    }
}
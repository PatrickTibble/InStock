using InStock.Common.InventoryService.Abstraction.TransferObjects.Get;
namespace InStock.Frontend.API.Mocks
{
    public class MockInventoryItem
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

        public GetResponse ToResponseModel()
            => new GetResponse
            {
                Id = Id,
                Name = Name,
                Description = Description,
                IsSuccessfulStatusCode = true
            };
    }
}
using InStock.Frontend.API.Models.Base;

namespace InStock.Frontend.API.Models.Inventory.Get
{
    public class Response : IResponse
	{
        public int Id { get; internal set; }
        public string? Name { get; internal set; }
        public string? Description { get; internal set; }

        public bool IsSuccessfulStatusCode { get; internal set; }
    }
}
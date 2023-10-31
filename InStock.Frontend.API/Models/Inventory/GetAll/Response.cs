using InStock.Frontend.API.Models.Base;

namespace InStock.Frontend.API.Models.Inventory.GetAll
{
    public class Response : IResponse
	{
        public bool IsSuccessfulStatusCode { get; set; }
        public IList<Get.Response>? Items { get; internal set; }
    }
}
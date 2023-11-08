using InStock.Common.IdentityService.Abstraction.Entities;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.Addresses
{
    public class AddressesResponse
    {
        public IList<Address>? Addresses { get; set; }
    }
}
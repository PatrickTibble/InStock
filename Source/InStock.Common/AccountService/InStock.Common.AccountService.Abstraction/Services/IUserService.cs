using InStock.Backend.Common.Attributes;
using InStock.Common.AccountService.Abstraction.TransferObjects.AddAddress;
using InStock.Common.AccountService.Abstraction.TransferObjects.Addresses;
using InStock.Common.AccountService.Abstraction.TransferObjects.UserProfile;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.AccountService.Abstraction.Services
{
    public interface IUserService
    {
        [Post($"/{Constants.UserAddress}")]
        Task<Result<AddAddressResponse>> AddUserAddressAsync([AccessTokenHeader] string accessToken, [Body] AddAddressRequest request);

        [Get($"/{Constants.UserAddresses}")]
        Task<Result<AddressesResponse>> GetUserAddressesAsync([AccessTokenHeader] string accessToken);

        [Get($"/{Constants.UserProfile}")]
        Task<Result<UserProfileResponse>> GetUserProfileAsync([AccessTokenHeader] string accessToken);
    }
}
using InStock.Common.IdentityService.Abstraction.Entities;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface ITokenService
    {
        string? CreateToken(UserToken userToken);
        UserToken ReadToken(string? token);
    }
}
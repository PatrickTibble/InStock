namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface IHashService
    {
        void CreateHash(string password, out byte[] hash, out byte[] salt);
        bool VerifyHash(string password, byte[] hash, byte[] salt);
    }
}
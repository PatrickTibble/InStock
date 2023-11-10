using InStock.Common.AccountService.Abstraction.Services;
using System.Security.Cryptography;
using System.Text;

namespace InStock.Backend.AccountService.Core.Services
{
    public class HmacSha512HashService : IHashService
    {
        public void CreateHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
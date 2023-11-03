using InStock.Backend.AccountService.Abstraction.Entities;
using InStock.Backend.AccountService.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace InStock.Backend.AccountService.Data.IdentityAccessManagement
{
    public class HashedUser
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

    internal class IdentityRepository : IIdentityRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IList<HashedUser> _users;

        public IdentityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _users = new List<HashedUser>();
        }

        public Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken)
        {
            var token = ReadToken(accessToken);
            
            if (token == null)
            {
                return Task.FromResult<IEnumerable<UserClaim>>(new List<UserClaim>());
            }

            return Task.FromResult(token.Claims.Select(c => c.ToUserClaim()));
        }

        public Task<string?> VerifyUserCredentialsAsync(string username, string password, IList<string> claims)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return Task.FromResult<string?>(default);
            }

            var passVerified = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

            if (!passVerified)
            {
                return Task.FromResult<string?>(default);
            }

            var token = CreateToken(new UserToken
            {
                Username = username,
                Claims = new List<UserClaim>
                {
                    UserClaim.Session_Read
                }
            });

            return Task.FromResult(token);
        }

        // The following methods were taken from ais.com
        // https://www.ais.com/how-to-generate-a-jwt-token-using-net-6/
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string? CreateToken(UserToken userToken)
        {
            var userClaims = userToken.Claims.Select(c => c.ToString().Replace("_", ".").ToLower());
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userToken.Username!),
                new Claim(ClaimTypes.Role, userToken.Role),
                new Claim("claims", string.Join(",", userClaims))
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                                   claims: claims,
                                   expires: userToken.Expiry,
                                   signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        // Except this one
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private JwtSecurityToken? ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

            if (jwt.SigningCredentials.Equals(key))
            {
                return jwt;
            }
            return default;
        }
    }
}
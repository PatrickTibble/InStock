using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class IdentityRepository : IIdentityRepository
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
            var jwt = ReadToken(accessToken);

            // if token is null or expired, return empty claims
            if (jwt == default)
            {
                return Task.FromResult<IEnumerable<UserClaim>>(new List<UserClaim>());
            }
            var claims = jwt.Claims.FirstOrDefault(c => c.Type.Equals("claims"))?.Value;
            return Task.FromResult(Parse(claims));
        }

        private IEnumerable<UserClaim> Parse(string? claims)
        {
            var claimList = claims?.Split(',');
            if (claimList == null)
            {
                yield break;
            }
            foreach (var claim in claimList)
            {
                var formatted = new StringBuilder();
                int index = 0;
                foreach (var character in claim)
                {
                    if (index == 0)
                    {
                        formatted.Append(character.ToString().ToUpper());
                    }
                    else if (character == '.')
                    {
                        formatted.Append('_');
                        index = 0;
                        continue;
                    }
                    else
                    {
                        formatted.Append(character);
                    }
                    index++;
                }
                if (Enum.TryParse<UserClaim>(formatted.ToString(), out var userClaim))
                {
                    yield return userClaim;
                }
            }
        }

        public Task<string?> GetUsernameAsync(string accessToken)
        {
            var jwt = ReadToken(accessToken);

            if (jwt == default)
            {
                return Task.FromResult<string?>(null);
            }

            return Task.FromResult(jwt.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name))?.Value);
        }

        public Task<bool> RegisterUserAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username!.Equals(username));
            if (user != null)
            {
                return Task.FromResult(false);
            }

            var passwordHash = default(byte[]);
            var passwordSalt = default(byte[]);

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            user = new HashedUser
            {
                Username = username,
                Role = UserRole.User,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _users.Add(user);
            return Task.FromResult(true);
        }

        public Task<string?> VerifyUserCredentialsAsync(string username, string password, IList<string> claims)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return Task.FromResult<string?>(default);
            }

            var passVerified = false;
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passVerified = computedHash.SequenceEqual(user.PasswordHash);
            }

            if (!passVerified)
            {
                return Task.FromResult<string?>(default);
            }

            var jwt = CreateToken(new UserToken
            {
                Username = username,
                Role = user.Role.ToString(),
                Expiry = DateTime.UtcNow.AddHours(1),
                Claims = claims
            });

            return Task.FromResult(jwt);
        }

        private string? CreateToken(UserToken userToken)
        {
            var userClaims = userToken.Claims;
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userToken.Username!),    // todo: replace with session id that can be mapped to a user?
                new Claim(ClaimTypes.Role, userToken.Role),         // todo: do we need roles here?
                                                                    // todo: what other claims do we need?
                new Claim("claims", string.Join(",", userClaims))
            };

            var key = GetKey();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                                   claims: claims,
                                   expires: userToken.Expiry,
                                   signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);
            return jwt;
        }

        private JwtSecurityToken? ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanValidateToken)
            {
                // validate the token
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                try
                {
                    var result = handler.ValidateToken(token, validationParameters, out var validatedToken);
                    return validatedToken as JwtSecurityToken;
                }
                catch (SecurityTokenSignatureKeyNotFoundException)
                {
                    // don't throw. Log invalid key
                }
                catch (SecurityTokenMalformedException)
                {
                    // don't throw. Log invalid token
                }
                catch (SecurityTokenExpiredException)
                {
                    // don't throw. Notify expired token?
                }
            }

            return default;
        }

        private SymmetricSecurityKey GetKey()
            => new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetKeyValue()));

        private string GetKeyValue()
            => _configuration.GetSection("AppSettings:Token").Value!;
    }
}
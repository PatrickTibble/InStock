using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class JwtSecurityTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public JwtSecurityTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? CreateToken(UserToken userToken)
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

        public UserToken ReadToken(string? token)
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
                    if (validatedToken is JwtSecurityToken jwt)
                    {
                        return new UserToken
                        {
                            Role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                            Expiry = jwt.ValidTo,
                            Username = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                            Claims = jwt.Claims.FirstOrDefault(c => c.Type == "claims")?.Value?.Split(',').ToList()
                        };
                    }
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
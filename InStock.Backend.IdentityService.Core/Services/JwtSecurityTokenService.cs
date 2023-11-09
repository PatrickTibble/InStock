using InStock.Common.Abstraction.Logger;
using InStock.Common.Core.Extensions;
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class JwtSecurityTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public JwtSecurityTokenService(
            IConfiguration configuration,
            ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public AccessRefreshTokenPair? CreateAccessRefreshTokenPair(UserToken userToken)
        {
            var accessToken = CreateToken(userToken);
            var refreshToken = CreateToken(new UserToken
            {
                Expiry = userToken.Expiry.AddHours(12)
            });

            if (accessToken is null || refreshToken is null)
            {
                return default;
            }

            return new AccessRefreshTokenPair
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string? CreateIdToken(string username, AccessRefreshTokenPair token)
        {
            var idToken = CreateToken(new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "username", username }
                }
            });

            return idToken;
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
                            Expiry = jwt.ValidTo,
                            Claims = new Dictionary<string, string>(jwt.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)))
                        };
                    }
                }
                catch (SecurityTokenSignatureKeyNotFoundException ex)
                {
                    // don't throw. Log invalid key
                    _logger
                        .LogExceptionAsync(ex)
                        .FireAndForgetSafeAsync();
                }
                catch (SecurityTokenMalformedException ex)
                {
                    // don't throw. Log invalid token
                    _logger
                        .LogExceptionAsync(ex)
                        .FireAndForgetSafeAsync();
                }
                catch (SecurityTokenExpiredException ex)
                {
                    // don't throw. Notify expired token?
                    _logger
                        .LogExceptionAsync(ex)
                        .FireAndForgetSafeAsync();
                }
            }

            return default;
        }

        public AccessRefreshTokenPair? RefreshWithTokenPair(AccessRefreshTokenPair request)
        {
            var userToken = ReadToken(request.AccessToken);
            if (userToken == default)
            {
                return default;
            }

            var refreshUserToken = ReadToken(request.RefreshToken);
            if (refreshUserToken == default)
            {
                return default;
            }

            userToken.Expiry = DateTime.UtcNow.AddMinutes(30);
            var tokenPair = CreateAccessRefreshTokenPair(userToken);
            return tokenPair;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var jwt = ReadToken(token);

                return jwt != default;
            }
            catch (Exception ex)
            {
                _logger
                    .LogExceptionAsync(ex)
                    .FireAndForgetSafeAsync();
            }

            return false;
        }

        private string? CreateToken(UserToken userToken)
        {
            var claims = userToken.Claims?.Select(c => new Claim(c.Key, c.Value));

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

        private SymmetricSecurityKey GetKey()
            => new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetKeyValue()));
        private string GetKeyValue()
            => _configuration.GetSection("AppSettings:Token").Value!;
    }
}
using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Core.Extensions;
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
        private readonly ILogger _logger;

        public JwtSecurityTokenService(
            IConfiguration configuration,
            ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task<string?> CreateAccessTokenAsync()
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "access" }
                }
            };

            return CreateTokenAsync(userToken);
        }

        public Task<string?> CreateIdentityTokenAsync(string username)
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "id" },
                    { "sub", username }
                }
            };

            return CreateTokenAsync(userToken);
        }

        public Task<string?> CreateRefreshTokenAsync()
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddHours(12),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "refresh" }
                }
            };

            return CreateTokenAsync(userToken);

        }

        public Task<string?> CreateTokenAsync(UserToken userToken)
        {
            try
            {
                var claims = userToken.Claims?.Select(c => new Claim(c.Key, c.Value));

                var key = GetKey();
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var jwt = new JwtSecurityToken(
                                       claims: claims,
                                       expires: userToken.Expiry,
                                       signingCredentials: credentials);

                var handler = new JwtSecurityTokenHandler();
                var token = handler.WriteToken(jwt);

                return Task.FromResult<string?>(token);
            }
            catch (Exception ex)
            {
                _logger
                    .LogExceptionAsync(ex)
                    .FireAndForgetSafeAsync();

                throw;
            }
        }

        public Task<UserToken?> ReadTokenAsync(string token)
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
                        return Task.FromResult<UserToken?>(new UserToken
                        {
                            Expiry = jwt.ValidTo,
                            Claims = new Dictionary<string, string>(jwt.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)))
                        });
                    }
                }
                catch (Exception ex)
                {
                    _logger
                        .LogExceptionAsync(ex)
                        .FireAndForgetSafeAsync();
                }
            }

            return Task.FromResult<UserToken?>(default);
        }

        private SymmetricSecurityKey GetKey()
            => new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetKeyValue()));
        private string GetKeyValue()
            => _configuration.GetSection("AppSettings:Token").Value!;
    }
}
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using System.Text;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IList<HashedUser> _users;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public IdentityRepository(IHashService hashService, ITokenService tokenService)
        {
            _hashService = hashService;
            _tokenService = tokenService;
            _users = new List<HashedUser>();
        }

        public Task<IEnumerable<UserClaim>> GetUserClaimsAsync(string accessToken)
        {
            var jwt = _tokenService.ReadToken(accessToken);

            // if token is null or expired, return empty claims
            if (jwt == default)
            {
                return Task.FromResult<IEnumerable<UserClaim>>(new List<UserClaim>());
            }

            return Task.FromResult(Parse(jwt.Claims));
        }

        private IEnumerable<UserClaim> Parse(IList<string>? claimList)
        {
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
            var jwt = _tokenService.ReadToken(accessToken);

            if (jwt == default)
            {
                return Task.FromResult<string?>(null);
            }

            return Task.FromResult(jwt.Username);
        }

        public Task<bool> RegisterUserAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username!.Equals(username));
            if (user != null)
            {
                return Task.FromResult(false);
            }

            _hashService.CreateHash(password, out var passwordHash, out var passwordSalt);

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

            var passVerified = _hashService.VerifyHash(password, user.PasswordHash, user.PasswordSalt);

            if (!passVerified)
            {
                return Task.FromResult<string?>(default);
            }

            var jwt = _tokenService.CreateToken(new UserToken
            {
                Username = username,
                Role = user.Role.ToString(),
                Expiry = DateTime.UtcNow.AddHours(1),
                Claims = claims
            });

            return Task.FromResult(jwt);
        }
    }
}
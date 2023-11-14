using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly string _connectionString;

        public TokenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("IdentityServer")!;
        }

        public Task<StoredAccessToken?> GetAccessTokenAsync(string accessToken)
        {
            var command = new SqlCommand("sp_GetAccessTokenFromTokenValue")
            {
                CommandType = CommandType.StoredProcedure 
            };

            command.Parameters.AddWithValue("@TokenValue", accessToken);
            var storedToken = default(StoredAccessToken);
            ExecuteCommand(command, (reader) =>
            {
                if (reader.HasRows && reader.Read())
                {
                    storedToken = new StoredAccessToken
                    {
                        Id = reader.GetInt32(0),
                        IdentityTokenId = reader.GetInt32(1),
                        Invalidated = reader.GetBoolean(2),
                        TokenValue = reader.GetString(3)
                    };
                }
            });

            return Task.FromResult(storedToken);
        }

        public Task<StoredToken?> GetIdentityTokenAsync(string idToken)
        {
            var command = new SqlCommand("sp_GetIdentityTokenFromTokenValue")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TokenValue", idToken);
            return GetStoredToken(command);
        }

        public Task<StoredToken?> GetIdentityTokenAsync(int identityTokenId)
        {
            var command = new SqlCommand("sp_GetIdentityTokenFromId")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdentityTokenId", identityTokenId);
            return GetStoredToken(command);
        }

        public Task<StoredRefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            var command = new SqlCommand("sp_GetRefreshTokenFromTokenValue")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TokenValue", refreshToken);
            var storedToken = default(StoredRefreshToken);
            ExecuteCommand(command, (reader) =>
            {
                if (reader.HasRows && reader.Read())
                {
                    storedToken = new StoredRefreshToken
                    {
                        Id = reader.GetInt32(0),
                        AccessTokenId = reader.GetInt32(1),
                        Invalidated = reader.GetBoolean(2),
                        TokenValue = reader.GetString(3)
                    };
                }
            });

            return Task.FromResult(storedToken);
        }

        public Task InvalidateTokenFamilyAsync(StoredToken storedToken)
        {
            var command = default(SqlCommand);
            if (storedToken is StoredRefreshToken storedRefreshToken)
            {
                command = new SqlCommand("sp_InvalidateTokenFamilyForRefreshToken")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@RefreshTokenId", storedRefreshToken.Id);
            }
            else
            {
                command = new SqlCommand("sp_InvalidateTokenFamily")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdentityTokenId", storedToken.Id);
            }

            ExecuteCommand(command);

            return Task.CompletedTask;
        }

        public Task<bool> InvalidateTokensAsync(StoredRefreshToken storedRefreshToken, StoredAccessToken storedAccessToken)
        {
            var command = new SqlCommand("sp_InvalidateTokens")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@RefreshTokenId", storedRefreshToken.Id);
            command.Parameters.AddWithValue("@AccessTokenId", storedAccessToken.Id);

            var result = ExecuteCommand(command);

            return Task.FromResult(result);
        }

        public Task SaveTokensAsync(string idToken, string newAccessToken, string newRefreshToken)
        {
            var command = new SqlCommand("sp_SaveTokens")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdentityToken", idToken);
            command.Parameters.AddWithValue("@AccessToken", newAccessToken);
            command.Parameters.AddWithValue("@RefreshToken", newRefreshToken);

            ExecuteCommand(command);

            return Task.CompletedTask;
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            var command = new SqlCommand("sp_ValidateToken")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TokenValue", token);

            var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            ExecuteCommand(command);

            var result = returnParameter.Value != null && (int)returnParameter.Value == 1;

            return Task.FromResult(result);
        }

        private bool ExecuteCommand(SqlCommand command, Action<SqlDataReader>? callback = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;
                connection.Open();
                var result = false;
                if (callback != null)
                {
                    var reader = command.ExecuteReader();
                    callback.Invoke(reader);
                    reader.Close();
                    return true;
                }
                else
                {
                    result = command.ExecuteNonQuery() > 0;
                }
                connection.Close();
                return result;
            }
        }

        private Task<StoredToken?> GetStoredToken(SqlCommand command)
        {
            var storedToken = default(StoredToken);
            ExecuteCommand(command, (reader) =>
            {
                if (reader.HasRows && reader.Read())
                {
                    storedToken = new StoredToken
                    {
                        Id = reader.GetInt32(0),
                        Invalidated = reader.GetBoolean(1),
                        TokenValue = reader.GetString(2)
                    };
                }
            });

            return Task.FromResult(storedToken);
        }
    }
}
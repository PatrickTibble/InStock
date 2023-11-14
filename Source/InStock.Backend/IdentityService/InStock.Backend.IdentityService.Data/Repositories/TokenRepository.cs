using InStock.Backend.Utilities.DatabaseHelper;
using InStock.Common.Abstraction.Converters;
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
        private readonly IExecutor<SqlCommand, SqlDataReader> _executor;
        private readonly IConverter<SqlDataReader> _converter;

        public TokenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("IdentityServer")!;
            _executor = new CommandExecutor(_connectionString);
            _converter = new SqlDataReaderRowConverter();
        }

        public Task<StoredAccessToken?> GetAccessTokenAsync(string accessToken)
        {
            var command = new SqlCommand("sp_GetAccessTokenFromTokenValue")
            {
                CommandType = CommandType.StoredProcedure 
            };

            command.Parameters.AddWithValue("@TokenValue", accessToken);

            var storedToken = _executor
                .Execute<StoredAccessToken>(command, _converter)?
                .FirstOrDefault();

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

            var storedToken = _executor
                .Execute<StoredRefreshToken>(command, _converter)?
                .FirstOrDefault();

            return Task.FromResult(storedToken);
        }

        public Task InvalidateTokenFamilyAsync(StoredToken storedToken)
        {
            SqlCommand? command;
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

            _executor.Execute(command);

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

            var result = _executor.Execute(command) > 0;

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

            _executor.Execute(command);

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

            _executor.Execute(command);

            var result = returnParameter.Value != null && (int)returnParameter.Value == 1;

            return Task.FromResult(result);
        }

        private Task<StoredToken?> GetStoredToken(SqlCommand command)
        {
            var storedToken = _executor
                .Execute<StoredToken>(command, _converter)?
                .FirstOrDefault();

            return Task.FromResult(storedToken);
        }
    }
}
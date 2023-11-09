using Azure.Core;
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IConfiguration _configuration;

        public IdentityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Token?> GetIdTokenAsync(string accessToken)
        {
            using var connection = GetConnection();
            var command = new SqlCommand("GetIdentityTokenFromAccessToken", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@AccessToken", accessToken);

            command.Connection.Open();

            var reader = command.ExecuteReader();

            Token token = default;
            
            if (reader.Read())
            {
                token = new Token
                {
                    Id = reader.GetInt32(0),
                    TokenValue = reader.GetString(1),
                    Invalidated = reader.GetBoolean(2)
                };
            }

            reader.Close();

            command.Connection.Close();

            return Task.FromResult(token);
        }

        public Task<bool> SaveTokenPairAsync(string accessToken, int idTokenId, string refreshToken)
        {
            using var connection = GetConnection();
            var command = new SqlCommand("InsertAccessRefreshTokenPair", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@AccessToken", accessToken);
            command.Parameters.AddWithValue("@IdentityTokenId", idTokenId);
            command.Parameters.AddWithValue("@RefreshToken", refreshToken);

            command.Connection.Open();

            var reader = command.ExecuteReader();
            var result = reader.RecordsAffected > 0;
            reader.Close();

            command.Connection.Close();

            return Task.FromResult(result);
        }

        public Task<bool> StoreTokensAsync(string idToken, AccessRefreshTokenPair tokenPair)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            // ensure the token exists in our db (and is still valid)
            using var connection = GetConnection();
            var command = new SqlCommand("SELECT * FROM [dbo].[Token] WHERE Value = @Token AND NOT Invalidated;", connection);
            command.Parameters.AddWithValue("@Token", token);

            command.Connection.Open();

            var reader = command.ExecuteReader();
            var isValid = reader.HasRows;
            reader.Close();

            command.Connection.Close();

            return Task.FromResult(isValid);
        }

        public Task<bool> ValidateTokenPairAsync(AccessRefreshTokenPair request)
        {
            // ensure the tokens exist, are not invalidated, and that this refresh token is associated with the access token
            using var connection = GetConnection();

            var command = new SqlCommand(@"
                    SELECT * FROM [dbo].[Token] WHERE Value = @RefreshToken AND NOT Invalidated 
                    AND AccessRefreshTokenPairId = (SELECT Id FROM [dbo].[RefreshTokens] WHERE AccessToken = @AccessToken AND NOT Invalidated);
                ", connection);
            throw new NotImplementedException();
        }

        private SqlConnection GetConnection()
            => new SqlConnection(_configuration.GetConnectionString("IdentityServer"));
    }
}
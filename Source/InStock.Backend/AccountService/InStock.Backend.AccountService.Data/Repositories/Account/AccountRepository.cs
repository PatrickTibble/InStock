using InStock.Backend.Utilities.DatabaseHelper;
using InStock.Common.Abstraction.Converters;
using InStock.Common.AccountService.Abstraction.Entities;
using InStock.Common.AccountService.Abstraction.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InStock.Backend.AccountService.Data.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        private readonly IExecutor<SqlCommand, SqlDataReader> _executor;
        private readonly IConverter<SqlDataReader> _converter;

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AccountServer")!;
            _executor = new CommandExecutor(_connectionString);
            _converter = new SqlDataReaderRowConverter();
        }

        public Task<UserAccount?> GetUserByUsernameAsync(string? username)
        {
            var command = new SqlCommand("sp_GetUserFromUsername")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);

            var user = _executor
                .Execute<UserAccount>(command, _converter)
                .FirstOrDefault();

            return Task.FromResult(user);
        }

        public Task AddUserAsync(string? firstName, string? lastName, string? username, byte[] hash, byte[] salt)
        {
            var command = new SqlCommand("sp_AddUserAndProfile")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@PasswordHash", hash);
            command.Parameters.AddWithValue("@PasswordSalt", salt);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);

            _executor.Execute(command);

            return Task.CompletedTask;
        }

        public Task<HashedUser?> GetHashedUserByUsernameAsync(string? username)
        {
            var command = new SqlCommand("sp_GetHashedUserFromUsername")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);

            var user = _executor
                .Execute<HashedUser>(command, _converter)
                .FirstOrDefault();

            return Task.FromResult(user);
        }

        public Task AddUserTokenForClientAsync(string username, Guid clientId, string clientName, string clientDescription, string identityToken)
        {
            var command = new SqlCommand("sp_AddUserTokenForClient")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@ClientId", clientId);
            command.Parameters.AddWithValue("@ClientName", clientName);
            command.Parameters.AddWithValue("@ClientDescription", clientDescription);
            command.Parameters.AddWithValue("@IdentityToken", identityToken);

            _executor.Execute(command);

            return Task.CompletedTask;
        }

        public Task<IList<string>> GetUserTokensForClientAsync(Guid guid)
        {
            var command = new SqlCommand("sp_GetUserTokensForClient")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ClientId", guid);

            var tokens = _executor
                .Execute<string>(command, _converter);

            return Task.FromResult(tokens);
        }
    }
}
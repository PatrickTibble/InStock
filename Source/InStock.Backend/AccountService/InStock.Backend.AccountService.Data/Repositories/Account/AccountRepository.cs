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

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AccountServer")!;
        }

        public Task<UserAccount?> GetUserByUsernameAsync(string? username)
        {
            var command = new SqlCommand("sp_GetUserFromUsername")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);

            var user = default(UserAccount);
            ExecuteCommand(command, reader =>
            {
                user = new UserAccount
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Username = reader.GetString(3)
                };
            });

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

            var id = 0;
            ExecuteCommand(command);

            return Task.CompletedTask;
        }

        public Task<HashedUser?> GetHashedUserByUsernameAsync(string? username)
        {
            var command = new SqlCommand("sp_GetHashedUserFromUsername")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);

            var user = default(HashedUser);
            ExecuteCommand(command, reader =>
            {
                user = new HashedUser
                {
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetFieldValue<byte[]>(2),
                    PasswordSalt = reader.GetFieldValue<byte[]>(3)
                };
            });

            return Task.FromResult(user);
        }

        private void ExecuteCommand(SqlCommand command, Action<SqlDataReader>? callback = null)
        {
            using var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.Connection.Open();
            if (callback != null)
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        callback(reader);
                    }
                }
                reader.Close();
            }
            else
            {
                command.ExecuteNonQuery();
            }
            command.Connection.Close();
        }

        public Task AddUserClientAsync(string username, string clientId, string identityToken)
        {
            var command = new SqlCommand("sp_AddUserClient")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@ClientId", clientId);
            command.Parameters.AddWithValue("@IdentityToken", identityToken);

            ExecuteCommand(command);

            return Task.CompletedTask;
        }
    }
}
using InStock.Common.AccountService.Abstraction.Entities;
using InStock.Common.AccountService.Abstraction.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace InStock.Backend.AccountService.Data.Repositories.Account
{
    public class DBAccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;

        private const string TABLE_NAME = "[dbo].[Accounts]";

        public DBAccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<bool> AddUserAsync(UserAccount user)
        {
            using var connection = GetConnection();
            var command = new SqlCommand($"INSERT INTO {TABLE_NAME} (Username, FirstName, LastName) VALUES (@Username, @FirstName, @LastName)", connection);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            return Task.FromResult(command.ExecuteNonQuery() > 0);
        }

        public Task<bool> DeleteUserAsync(UserAccount user)
        {
            return Task.FromResult(false);
        }

        public Task<UserAccount?> GetUserByIdAsync(int id)
            => Task.FromResult(QueryUserAccount("Id", id));

        public Task<UserAccount?> GetUserByUsernameAsync(string? username)
            => Task.FromResult(QueryUserAccount("Username", username));

        public Task<bool> UpdateUserAsync(UserAccount user)
        {
            var connection = GetConnection();
            var command = new SqlCommand($"UPDATE {TABLE_NAME} SET Username = @Username, FirstName = @FirstName, LastName = @LastName WHERE Id = @Id", connection);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Id", user.Id);
            // Shouldn't be able to change username.. but maybe?
            // command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            return Task.FromResult(command.ExecuteNonQuery() > 0);
        }

        private SqlConnection GetConnection()
            => new SqlConnection(_configuration.GetConnectionString("AccountServer"));

        private UserAccount? QueryUserAccount<TValueType>(string paramName, TValueType value)
        {
            using var connection = GetConnection();
            var command = new SqlCommand($"SELECT * FROM {TABLE_NAME} WHERE {paramName} = @{paramName}", connection);
            command.Connection.Open();
            command.Parameters.AddWithValue($"@{paramName}", value);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new UserAccount
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    FirstName = reader.GetString(2),
                    LastName = reader.GetString(3)
                };
            }
            return default;
        }
    }
}
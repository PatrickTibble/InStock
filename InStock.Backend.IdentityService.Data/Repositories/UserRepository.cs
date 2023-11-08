using InStock.Common.IdentityService.Abstraction.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace InStock.Backend.IdentityService.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<int> CreateUserAsync(string username, string firstName, string lastName, byte[] passwordHash, byte[] passwordSalt)
        {
            using var connection = GetConnection();

            var userId = CreateUser(connection, username, passwordHash, passwordSalt);

            if (userId <= 0)
            {
                return Task.FromResult(-1);
            }

            var profileCreated = CreateProfile(connection, userId, firstName, lastName);

            return Task.FromResult(profileCreated ? userId : -1);
        }

        public Task<bool> GetUsernameAvailableAsync(string? username)
        {
            using var connection = GetConnection();
            var command = new SqlCommand($"SELECT COUNT(*) FROM [dbo].[User] WHERE Username = @Username", connection);
            command.Parameters.AddWithValue("@Username", username);

            command.Connection.Open();
            var isAvailable = (int)command.ExecuteScalar() == 0;
            command.Connection.Close();

            return Task.FromResult(isAvailable);
        }

        private SqlConnection GetConnection()
            => new SqlConnection(_configuration.GetConnectionString("IdentityServer")!);

        private int CreateUser(SqlConnection connection, string username, byte[] passwordHash, byte[] passwordSalt)
        {
            var command = new SqlCommand($"INSERT INTO [dbo].[User] (Username, PasswordHash, PasswordSalt) VALUES (@Username, @PasswordHash, @PasswordSalt); " +
                "SELECT CAST(scope_identity() AS int);", connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);

            command.Connection.Open();
            var userId = (int)command.ExecuteScalar();
            command.Connection.Close();

            return userId;
        }

        private bool CreateProfile(SqlConnection connection, int userId, string firstName, string lastName)
        {
            var command = new SqlCommand($"INSERT INTO [dbo].[UserProfile] (UserId, FirstName, LastName) VALUES (@UserId, @FirstName, @LastName);", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);

            command.Connection.Open();
            var created = command.ExecuteNonQuery() > 0;
            command.Connection.Close();

            return created;
        }
    }
}
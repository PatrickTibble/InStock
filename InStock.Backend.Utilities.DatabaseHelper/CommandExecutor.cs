using InStock.Common.Abstraction.Converters;
using Microsoft.Data.SqlClient;

namespace InStock.Backend.Utilities.DatabaseHelper
{
    public class CommandExecutor : IExecutor<SqlCommand, SqlDataReader>
    {
        private string _connectionString;

        public CommandExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<T> Execute<T>(SqlCommand command, IConverter<SqlDataReader> converter)
            where T : class
        {
            var result = new List<T>();

            using var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.Connection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(converter.Convert<T>(reader));
                }
            }
            reader.Close();
            command.Connection.Close();
            return result;
        }

        public int Execute(SqlCommand command)
        {
            var result = 0;
            using var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.Connection.Open();
            result = command.ExecuteNonQuery();
            command.Connection.Close();
            return result;
        }
    }
}
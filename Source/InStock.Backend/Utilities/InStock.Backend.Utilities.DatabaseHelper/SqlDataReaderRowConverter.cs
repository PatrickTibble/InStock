using InStock.Common.Abstraction.Converters;
using Microsoft.Data.SqlClient;

namespace InStock.Backend.Utilities.DatabaseHelper
{
    public class SqlDataReaderRowConverter : IConverter<SqlDataReader>
    {
        public T Convert<T>(SqlDataReader reader)
            where T : class
        {
            var properties = typeof(T).GetProperties();
            var instance = Activator.CreateInstance<T>();
            foreach (var property in properties)
            {
                var value = reader[property.Name];
                if (value != DBNull.Value)
                {
                    property.SetValue(instance, value);
                }
            }
            return instance;
        }
    }
}
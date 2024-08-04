using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace StagecraftDAL
{
    public static class PostgreSQLDataAccess
    {

        private static string _connection;
        public static IConfiguration? _config { get; set; }
        static PostgreSQLDataAccess()
        {

            _config = Configuration.ReadConfigValue();
            _connection = _config["ConnectionStrings:PostgreSqlConnection"];
        }
        public static List<T> ExecuteFunction<T>(string functionName, params NpgsqlParameter[] parameters) where T : new()
        {
            List<T> result = new List<T>();

            using (var connection = new NpgsqlConnection(_connection))
            {
                connection.Open();

                // הכנה לפקודה עם שימוש בפרמטרים
                var parameterPlaceholders = parameters != null
                    ? string.Join(", ", parameters.Select((p, i) => $"@p{i}"))
                    : string.Empty;

                using (var command = new NpgsqlCommand($"SELECT * FROM {functionName}({parameterPlaceholders});", connection))
                {
                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@p{i}", parameters[i].Value);
                        }
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T obj = new T();
                            MapReaderToObj(reader, obj);
                            result.Add(obj);
                        }
                    }
                }
            }

            return result;
        }
        public static T ExecuteSimpleTypeFunction<T>(string functionName, List<NpgsqlParameter> parameters = null)
        {
            using (var connection = new NpgsqlConnection(_connection))
            {
                connection.Open();

                // הכנה לפקודה עם שימוש בפרמטרים
                var parameterPlaceholders = parameters != null
                    ? string.Join(", ", parameters.Select((p, i) => $"@p{i}"))
                    : string.Empty;

                using (var command = new NpgsqlCommand($"SELECT {functionName}({parameterPlaceholders});", connection))
                {
                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@p{i}", parameters[i].Value);
                        }
                    }

                    var result = command.ExecuteScalar();

                    return ConvertTo<T>(result);
                }
            }

        }

        private static T ConvertTo<T>(object value)
        {
            if (value == DBNull.Value || value == null)
            {
                return default;
            }

            // Handle different types
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)Convert.ToBoolean(value);
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(object)Convert.ToInt32(value);
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(object)value.ToString();
            }

            // Add more type conversions as needed
            throw new InvalidCastException($"Cannot convert type {value.GetType()} to {typeof(T)}");
        }

        private static void MapReaderToObj<T>(NpgsqlDataReader reader, T obj) where T : new()
        {
            var properties = typeof(T).GetProperties();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var property = properties.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));

                if (property != null && !reader.IsDBNull(i))
                {
                    property.SetValue(obj, reader.GetValue(i));
                }
            }
        }
    }
}




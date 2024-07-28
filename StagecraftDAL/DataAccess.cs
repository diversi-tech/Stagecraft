//namespace StagecraftDAL
//{
//    using System;
//    using System.Data;
//    using System.Data.SqlClient;
//    using Microsoft.Extensions.Configuration;


//    public static class DataAccess
//    {

//        private static string _connection;
//        public static IConfiguration? _config { get; set; }
//        static DataAccess()
//        {

//            _config = Configuration.ReadConfigValue();
//            _connection = _config["ConnectionStrings:DefaultConnection"];
//        }
//        public static T ExecuteStoredProcedure<T>(string storedProcedureName, params SqlParameter[] parameters)
//        {
//            using (

//                var connection = new SqlConnection(_connection))
//            using (var command = new SqlCommand(storedProcedureName, connection))
//            {
//                command.CommandType = CommandType.StoredProcedure;
//                if (parameters != null)
//                {
//                    command.Parameters.AddRange(parameters);
//                }

//                connection.Open();

//                //if (command.Parameters["@CourseId"] == null)
//                //{
//                //    throw new InvalidOperationException("Parameter '@CourseId' was not supplied.");
//                //}
//                object myresult = command.ExecuteScalar();
//                using (SqlDataReader dr = command.ExecuteReader())

//                using (SqlDataReader reader = command.ExecuteReader())

//                {
//                    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
//                    {
//                        // Handle IEnumerable<T>
//                        Type itemType = typeof(T).GetGenericArguments()[0];
//                        var method = typeof(DataMapper).GetMethod(nameof(DataMapper.MapToList)).MakeGenericMethod(itemType);
//                        var result = method.Invoke(null, new object[] { reader });
//                        return (T)result;
//                    }
//                    else if (reader.Read())
//                    {
//                        var result = Activator.CreateInstance<T>();
//                        foreach (var prop in typeof(T).GetProperties())
//                        {
//                            if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
//                            {
//                                prop.SetValue(result, reader.GetValue(reader.GetOrdinal(prop.Name)));
//                            }
//                        }
//                        return result;
//                    }
//                    else
//                    {
//                        throw new InvalidOperationException("No rows found.");
//                    }
//                }
//            }
//        }
//        public static void ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
//        {
//            using (var connection = new SqlConnection(_connection))
//            using (var command = new SqlCommand(storedProcedureName, connection))
//            {
//                command.CommandType = CommandType.StoredProcedure;
//                if (parameters != null)
//                {
//                    command.Parameters.AddRange(parameters);
//                }

//                connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }



//    }
//}

namespace StagecraftDAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;


    public static class DataAccess
    {

        private static string _connection;
        public static IConfiguration? _config { get; set; }
        static DataAccess()
        {

            _config = Configuration.ReadConfigValue();
            _connection = _config["ConnectionStrings:DefaultConnection"];
        }
        public static T ExecuteStoredProcedure<T>(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connection))
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();

                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                {
                    // Handle IEnumerable<T>
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        Type itemType = typeof(T).GetGenericArguments()[0];
                        var method = typeof(DataMapper).GetMethod(nameof(DataMapper.MapToList)).MakeGenericMethod(itemType);
                        var result = method.Invoke(null, new object[] { dr });
                        return (T)result;
                    }
                }
                else
                {
                    // Handle single value
                    object myresult = command.ExecuteScalar();
                    if (myresult != null && myresult != DBNull.Value)
                    {
                        return (T)Convert.ChangeType(myresult, typeof(T));
                    }
                    else
                    {
                        //ריקי ביקשה לשנות להחזרת ערכי ברירת מחדל במקום NULL
                        throw new InvalidOperationException("Unsupported return type");
                    }
                }
            }
        }

        public static void ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connection))
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal static IDisposable ExecuteStoredProcedureReader(string v, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}




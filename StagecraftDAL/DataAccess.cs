using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
namespace StagecraftDAL
{


    public static class DataAccess
    {

        private static string _connection;
        public static IConfiguration? _config { get; set; }
        //private static string ConnectionString = connection.ConnectionString;

        static DataAccess()
        {

            _config = Configuration.ReadConfigValue();
            _connection = _config["ConnectionStrings:DefaultConnection"];
        }

        //public static IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, params SqlParameter[] parameters) where T : new()
        //{
        //    List<T> result = new List<T>();
        //    using (var connection = new SqlConnection(_connection))
        //    using (var command = new SqlCommand(storedProcedureName, connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        if (parameters != null)
        //        {
        //            command.Parameters.AddRange(parameters);
        //        }

        //        connection.Open();
        //        using (SqlDataReader dr = command.ExecuteReader())
        //        {
        //            result = DataMapper.MapToList<T>(dr);
        //        }
        //    }
        //    return result;
        //}




        // פונקציה גנרית שמבצעת קריאה ל-stored procedure ומחזירה ערך גנרי
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
                object myresult = command.ExecuteScalar();
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                    {
                        // Handle IEnumerable<T>
                        Type itemType = typeof(T).GetGenericArguments()[0];
                        var method = typeof(DataMapper).GetMethod(nameof(DataMapper.MapToList)).MakeGenericMethod(itemType);
                        var result = method.Invoke(null, new object[] { dr });
                        return (T)result;
                    }

                    else if (typeof(T) is Course || typeof(T) is Users)
                    {
                        
                            var method = typeof(DataMapper).GetMethod(nameof(DataMapper.MapToObj)).MakeGenericMethod(typeof(T));
                            var result1 = method.Invoke(null, new object[] { dr });
                            return (T)result1;
                        
                    }
                    else if (myresult != null && myresult != DBNull.Value)
                    {
                        return (T)Convert.ChangeType(myresult, typeof(T));
                    }
                    else
                    {
                        throw new InvalidOperationException("Unsupported return type");
                    }
                }
            }
        }
    }
}






//    public static T ExecuteStoredProcedure<T>(string storedProcedureName, params SqlParameter[] parameters) where T : new()
//    {
//        using (var connection = new SqlConnection(_connection))
//        using (var command = new SqlCommand(storedProcedureName, connection))
//        {
//            command.CommandType = CommandType.StoredProcedure;


//              command.CommandType = CommandType.StoredProcedure;
//            if (parameters != null)
//            {
//                command.Parameters.AddRange(parameters);
//            }
//            // הוספת פרמטרים לפי הצורך
//            // command.Parameters.AddWithValue("@paramName", paramValue);
//            connection.Open();
//                // ביצוע הפרוצדורה והחזרת התוצאה שלה
//                object result = command.ExecuteScalar();
//            // אם מצפים להחזיר ערך מסוג T, יש לבצע המרה מתאימה

//            using (SqlDataReader dr = command.ExecuteReader())
//                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
//            {

//                // Handle IEnumerable<T>
//                Type itemType = typeof(T).GetGenericArguments()[0];
//                var method = typeof(DataMapper).GetMethod(nameof(DataMapper.MapToList)).MakeGenericMethod(itemType);
//                var result1 = method.Invoke(null, new object[] { dr });
//                return (T)result1;
//                           }
//                else if (typeof(T) == typeof(Users) || typeof(T) == typeof(Course))

//                else if (result != null && result != DBNull.Value)
//                {
//                    return (T)Convert.ChangeType(result, typeof(T));
//                }
//                else
//                {
//                    return default(T);
//                }
//            }
//        }

//    }
//}























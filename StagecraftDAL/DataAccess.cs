namespace StagecraftDAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;
    using Common;
    using System.Data.Common;

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

      //  public static IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, params SqlParameter[] parameters) where T : new()
      //  {
      //      List<T> result = new List<T>();
      //      using (var connection = new SqlConnection(_connection))
      //      using (var command = new SqlCommand(storedProcedureName, connection))
      //      {
      //          command.CommandType = CommandType.StoredProcedure;
      //          if (parameters != null)
      //          {
      //              command.Parameters.AddRange(parameters);
      //          }

      //          connection.Open();
      //          using (SqlDataReader dr = command.ExecuteReader())
      //          {
      //              result = DataMapper.MapToList<T>(dr);
      //          }
      //      }
      //      return result;
      //  }

      //}
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



    //public class DataAccess
    //{
    //    private  readonly string ConnectionString;

    //    public DataAccess(string connectionString)
    //    {
    //       ConnectionString = connectionString;
    //    }

    //    public static T ExecuteStoredProcedure<T>(string storedProcedureName, Func<IDataReader, T> mapFunction, params SqlParameter[] parameters)
    //    {
    //        using (var connection = new SqlConnection(ConnectionString))
    //        using (var command = new SqlCommand(storedProcedureName, connection))
    //        {
    //            command.CommandType = CommandType.StoredProcedure;
    //            command.Parameters.AddRange(parameters);

    //            connection.Open();
    //            using (var reader = command.ExecuteReader())
    //            {
    //                return mapFunction(reader);
    //            }
    //        }
    //    }
    //}




    //public class MyClass
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    // אפשר להוסיף פרטים נוספים לפי הצורך
    //}

    //public class Program
    //{
    //    public static void Main()
    //    {
    //        string connectionString = "YourConnectionStringHere";
    //        DataAccess dataAccess = new DataAccess(connectionString);

    //        SqlParameter parameter1 = new SqlParameter("@Param1", SqlDbType.Int);
    //        parameter1.Value = 1;

    //        SqlParameter parameter2 = new SqlParameter("@Param2", SqlDbType.VarChar);
    //        parameter2.Value = "SomeValue";

    //        MyClass result = dataAccess.ExecuteStoredProcedure(
    //            "YourStoredProcedureName",
    //            reader => new MyClass
    //            {
    //                Id = Convert.ToInt32(reader["Id"]),
    //                Name = reader["Name"].ToString(),
    //                // ניתן להוסיף פרטים נוספים כאן לפי הצורך
    //            },
    //            parameter1,
    //            parameter2);

    //        Console.WriteLine($"Id: {result.Id}, Name: {result.Name}");
    //    }
    //}

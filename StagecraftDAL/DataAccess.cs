namespace StagecraftDAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T ExecuteStoredProcedure<T>(string storedProcedureName, Func<IDataReader, T> mapFunction, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    return mapFunction(reader);
                }
            }
        }
    }

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
}
using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;



namespace StagecraftDAL.Services
    {
        public class UsersService : IUserDAL
        {
            private readonly string _connectionString;

            public UsersService(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            public double GetUserProgress(int userId, int courseId)
            {
            double progressPercentage = 0;

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("GetUserProgress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@CourseId", courseId);

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                progressPercentage = (int)reader.GetDouble(reader.GetOrdinal("ProgressPercentage"));
                            }
                        }
                    }
                }

                return progressPercentage;
            }
        public static bool CheckIfEmailExist(string email)
        {
            SqlParameter param1 = new SqlParameter("@email", email);
            var t = DataAccess.ExecuteStoredProcedure<bool>("checkIfEmailExist", param1);
            return t;
        }
    }
}









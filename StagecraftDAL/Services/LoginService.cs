using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StagecraftDAL.Interface;


namespace StagecraftDAL.Services
{
    public class LoginService : ILogin
    {

        public int CheckUserExistence(Users user)
        {

            //  await connection.OpenAsync();

            NpgsqlParameter param1 = new NpgsqlParameter("pemail", user.Email);
            NpgsqlParameter param2 = new NpgsqlParameter("ppassword_hash", user.Password);
            var t = PostgreSQLDataAccess.ExecuteSimpleTypeFunction<int>("check_user_existence", param1, param2);
            return t;
        }

        public List<Users> GetUserById(int userId)
        {
            NpgsqlParameter param1 = new NpgsqlParameter("puser_id", userId);
            var t = PostgreSQLDataAccess.ExecuteFunction<Users>("get_user_by_id", param1);
            return t;
        }
    }
}

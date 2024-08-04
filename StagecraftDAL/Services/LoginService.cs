using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;


namespace StagecraftDAL.Services
{
    public class LoginService : ILogin
    {

            public   int CheckUserExistence(Users user)
            {

            //  await connection.OpenAsync();

            SqlParameter param1 = new SqlParameter("@Email", user.Email);
            SqlParameter param2 = new SqlParameter("@Password", user.Password);
            var t = SQLDataAccess.ExecuteStoredProcedure<int>("CheckUserExistence", param1,param2);
            return t;
                    }
        public List<Users> GetUserById(int userId)
        {
            SqlParameter param1 = new SqlParameter("@user_id", userId);
            var t = SQLDataAccess.ExecuteStoredProcedure<List<Users>>("GetUserById", param1);
            return t;
        }
    }
            }

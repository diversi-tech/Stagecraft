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

            public   bool CheckUserExistence(Users user)
            {

            //  await connection.OpenAsync();

            SqlParameter param1 = new SqlParameter("@Email", user.Email);
            SqlParameter param2 = new SqlParameter("@Password", user.Password);
            var t = DataAccess.ExecuteStoredProcedure<bool>("CheckUserExistence", param1,param2);
            return t;
                    }
                }
            }

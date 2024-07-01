using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace StagecraftDAL.Services
{
    public class UsersService : IUserDAL
    {

        public static bool CheckIfEmailExist(string email)
        {
            SqlParameter param1 = new SqlParameter("@email", email);
            var t = DataAccess.ExecuteStoredProcedure<bool>("checkIfEmailExist", param1);
            return t;
        }

        public  int GetUserProgress(int  UserId,int CoursId )
        {
            SqlParameter param1 = new SqlParameter("@UserId", UserId ) ;
            SqlParameter param2 = new SqlParameter( "@CoursId", CoursId);
            var t = DataAccess.ExecuteStoredProcedure<int>("GetUserProgress", param1,param2);
            return t;
        }

    }
}

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
using Npgsql;



namespace StagecraftDAL.Services
{
    public class UsersService : IUser
    {

        public static bool CheckIfEmailExist(string email)
        {
            NpgsqlParameter param1 = new NpgsqlParameter("pemail_to_check", email);
            var t = PostgreSQLDataAccess.ExecuteSimpleTypeFunction<bool>("check_email", param1);
            return t;
        }

        public static void SignUp(Users user)
        {
            SqlParameter param1 = new SqlParameter("@Name", user.Name);
            SqlParameter param2 = new SqlParameter("@Email", user.Email);
            SqlParameter param3 = new SqlParameter("@CoursesNum", user.CoursesNum);
            SqlParameter param4 = new SqlParameter("@IsConfirmed", user.IsConfirmed);
            SqlParameter param5 = new SqlParameter("@CoursesNum", user.CoursesNum);
            SqlParameter param6 = new SqlParameter("@LastView", user.LastView);
            SqlParameter param7 = new SqlParameter("@Password", user.Password);
            SqlParameter param8 = new SqlParameter("@Status", user.Status);
            SqlParameter param9 = new SqlParameter("@registrationDate", user.LastModifiedDate);
            SqlParameter param10 = new SqlParameter("@LastModifiedDate", user.LastModifiedDate);
            var parameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10 };
            SQLDataAccess.ExecuteStoredProcedure("SignUpProcedure", parameters);
        }
        public static void SignUpForACourse(int courseId)
        {
            SqlParameter param1 = new SqlParameter("@courseId", courseId);
            SQLDataAccess.ExecuteStoredProcedure("SignUpForACourse", param1);

        }

        public int GetUserProgress(int UserId, int CourseId)
        {
            NpgsqlParameter param1 = new NpgsqlParameter("puser_id", UserId);
            NpgsqlParameter param2 = new NpgsqlParameter("pcourse_id", CourseId);
            var t = PostgreSQLDataAccess.ExecuteSimpleTypeFunction<int>("get_user_progress", param1, param2);
            return t;
        }
    }
}









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

           
        public static bool CheckIfEmailExist(string email)
        {
            SqlParameter param1 = new SqlParameter("@EmailToCheck", email);
            var t = DataAccess.ExecuteStoredProcedure<bool>("CheckEmail", param1);
            return t;
        }
        public static void SignUp(Users user)
        {   SqlParameter param1 = new SqlParameter("@Name", user.Name);
            SqlParameter param2 = new SqlParameter("@Email", user.Email); 
            SqlParameter param3 = new SqlParameter("@CoursesNum", user.CoursesNum);
            SqlParameter param4 = new SqlParameter("@IsConfirmed", user.IsConfirmed);
            SqlParameter param5 = new SqlParameter("@CoursesNum", user.CoursesNum);
            SqlParameter param6 = new SqlParameter("@LastView", user.LastView);
            SqlParameter param7 = new SqlParameter("@Password", user.Password);
            SqlParameter param8 = new SqlParameter("@Status", user.Status);
            SqlParameter param9 = new SqlParameter("@registrationDate", user.registrationDate);
            SqlParameter param10 = new SqlParameter("@LastModifiedDate", user.LastModifiedDate);
            var parameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10 };
             DataAccess.ExecuteStoredProcedure("SignUpProcedure", parameters);
        }
        public static void SignUpForACourse(int courseId)
        {
            SqlParameter param1 = new SqlParameter("@courseId", courseId);
             DataAccess.ExecuteStoredProcedure("SignUpForACourse", param1);
           
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









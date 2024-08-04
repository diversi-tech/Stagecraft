using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Npgsql;
using StagecraftDAL.Interface;


namespace StagecraftDAL.Services
{
    public class AdminService : IAdmin
    {
        private List<AdminCourse> _courses = new List<AdminCourse>();

        public List<AdminCourse> GetAllAdminCourses()
        {
            //var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("GetAllCourses", null);
            var t = PostgreSQLDataAccess.ExecuteFunction<AdminCourse>("get_all_courses");

            return t;
        }

        public AdminCourse GetAdminCourseById(int id)
        {
            //SqlParameter param1 = new SqlParameter("@CourseId", id);
            //var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("GetCourseById", param1).FirstOrDefault();
            NpgsqlParameter param1 = new NpgsqlParameter("course_id", id);
            var t = PostgreSQLDataAccess.ExecuteFunction<AdminCourse>("get_course_by_id", param1).FirstOrDefault();
            return t;
        }

        public List<AdminCourse> AddAdminCourses(AdminCourse course)
        {
            try
            {
                NpgsqlParameter[] parameters = [
                    new NpgsqlParameter("pcourse_id", course.courses_id),
                    new NpgsqlParameter("ptitle", course.title),
                    new NpgsqlParameter("pcourse_name", course.courses_name),
                    new NpgsqlParameter("pdescription", course.description),
                    new NpgsqlParameter("pprice", course.price),
                    new NpgsqlParameter("pseveral_chapters", course.Several_chapters),
                    new NpgsqlParameter("plength", course.Length),
                    new NpgsqlParameter("pnumber_of_viewers", course.numberOfViewers),
                    new NpgsqlParameter("pvideo_url", course.videoURL),
                    new NpgsqlParameter("ptask_files_url", course.taskFilesURL)
                ];

                var t = PostgreSQLDataAccess.ExecuteFunction<AdminCourse>("add_course_sp", parameters);
                return t;
            }
            catch (Exception ex)
            {
                return new List<AdminCourse>(); // החזרה של רשימה ריקה במקרה של שגיאה
            }
        }

        public List<AdminCourse> UpdateAdminCourses(int id, AdminCourse updatedCourse)
        {
            try
            {
                NpgsqlParameter param1 = new NpgsqlParameter("pcourse_id", id);
                NpgsqlParameter param3 = new NpgsqlParameter("ptitle", updatedCourse.title);
                NpgsqlParameter param2 = new NpgsqlParameter("pcourse_name", updatedCourse.courses_name);
                NpgsqlParameter param4 = new NpgsqlParameter("pdescription", updatedCourse.description);
                NpgsqlParameter param6 = new NpgsqlParameter("pprice", updatedCourse.price);
                NpgsqlParameter param7 = new NpgsqlParameter("pseveral_chapters", updatedCourse.Several_chapters);
                NpgsqlParameter param8 = new NpgsqlParameter("plength", updatedCourse.Length);
                NpgsqlParameter param9 = new NpgsqlParameter("pnumber_of_viewers", updatedCourse.numberOfViewers);
                NpgsqlParameter param11 = new NpgsqlParameter("pvideo_url", updatedCourse.videoURL);
                NpgsqlParameter param12 = new NpgsqlParameter("ptask_files_url", updatedCourse.taskFilesURL);

                var courses = PostgreSQLDataAccess.ExecuteFunction<AdminCourse>("update_course_sp", param1, param2, param3, param4, param6, param7, param8, param9, param11, param12);

                return courses;
                //SqlParameter param1 = new SqlParameter("@pCourseID", id);
                //SqlParameter param2 = new SqlParameter("@pCourseName", updatedCourse.courses_name);
                //SqlParameter param3 = new SqlParameter("@pTitle", updatedCourse.title);
                //SqlParameter param4 = new SqlParameter("@pDescription", updatedCourse.description);
                //SqlParameter param5 = new SqlParameter("@pUpdate_at", updatedCourse.update_at);
                //SqlParameter param6 = new SqlParameter("@pPrice", updatedCourse.price);
                //SqlParameter param7 = new SqlParameter("@pSeveral_chapters", updatedCourse.Several_chapters);
                //SqlParameter param8 = new SqlParameter("@pLength", updatedCourse.Length);
                //SqlParameter param9 = new SqlParameter("@pNumberOfViewers", updatedCourse.numberOfViewers);
                //SqlParameter param11 = new SqlParameter("@pVideoURL", updatedCourse.videoURL);
                //SqlParameter param12 = new SqlParameter("@pTaskFilesURL", updatedCourse.taskFilesURL);

                //var courses = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("UpdateCourse_SP", param1, param2, param3, param4, param5, param6, param7, param8, param9, param11, param12);

                //return courses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAdminCourses: {ex.Message}");
                return new List<AdminCourse>(); // החזרה של רשימה ריקה במקרה של שגיאה
            }
        }

        public List<AdminCourse> DeleteAdminCourse(int id)
        {
            NpgsqlParameter param1 = new NpgsqlParameter("pcourse_id", id);
            var t = PostgreSQLDataAccess.ExecuteFunction<AdminCourse>("delete_course_sp", param1);
            return t;
            //SqlParameter param1 = new SqlParameter("@pCourseID", id);
            //var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("DeleteCourse_SP", param1);
            //return t;
        }
        public List<Users> GetAllUsers()
        {
            var t = SQLDataAccess.ExecuteStoredProcedure<List<Users>>("GetAllUsers", null);
            return t;
        }

        public bool AddCoursToUser(UserCourses userCourses)
        {
            SqlParameter param1 = new SqlParameter("@userId", userCourses.userId);
            SqlParameter param2 = new SqlParameter("@coursId", userCourses.coursesId);
            SqlParameter param3 = new SqlParameter("@isApproved", userCourses.isApproved);
            var t = SQLDataAccess.ExecuteStoredProcedure<bool>("AddCoursToUser", param1, param2, param3);
            return t;
        }

        public bool DeletCoursToUser(UserCourses userCourses)
        {
            SqlParameter param1 = new SqlParameter("@userId", userCourses.userId);
            SqlParameter param2 = new SqlParameter("@coursId", userCourses.coursesId);
            var t = SQLDataAccess.ExecuteStoredProcedure<bool>("DeletCoursToUser", param1, param2);
            return t;
        }

        public List<Course> GetAllCoursOfUser(int userId)
        {
            SqlParameter param1 = new SqlParameter("@userId", userId);
            var t = SQLDataAccess.ExecuteStoredProcedure<List<Course>>("GetAllCoursOfUser", param1);
            return t;

        }
    }
}
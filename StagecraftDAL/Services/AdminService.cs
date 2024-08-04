using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using StagecraftDAL.Interface;


namespace StagecraftDAL.Services
{
    public class AdminService: IAdmin 
    {
        private List<AdminCourse> _courses = new List<AdminCourse>();

        public List<AdminCourse> GetAllAdminCourses()
        {
            var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("GetAllCourses", null);
            return t;
        }

        public AdminCourse GetAdminCourseById(int id)
        {
            SqlParameter param1 = new SqlParameter("@CourseId", id);
            var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("GetCourseById", param1).FirstOrDefault();
            return t;
        }

        public List<AdminCourse> AddAdminCourses(AdminCourse course)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@pCourseID", course.courses_id);
                SqlParameter param2 = new SqlParameter("@pCourseName", course.courses_name);
                SqlParameter param3 = new SqlParameter("@pTitle", course.title);
                SqlParameter param4 = new SqlParameter("@pDescription", course.description);
                SqlParameter param5 = new SqlParameter("@pCreate_at", course.create_at);
                SqlParameter param6 = new SqlParameter("@pUpdate_at", course.update_at);
                SqlParameter param7 = new SqlParameter("@pPrice", course.price);
                SqlParameter param8 = new SqlParameter("@pSeveral_chapters", course.Several_chapters);
                SqlParameter param9 = new SqlParameter("@pLength", course.Length);
                SqlParameter param10 = new SqlParameter("@pNumberOfViewers", course.numberOfViewers);
                SqlParameter param11 = new SqlParameter("@pVideoURL", course.videoURL);
                SqlParameter param12 = new SqlParameter("@pTaskFilesURL", course.taskFilesURL);

                var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("AddCourse_SP", param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12);


                Console.WriteLine(t);
                return t;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddAdminCourses: {ex.Message}");
                return new List<AdminCourse>(); // החזרה של רשימה ריקה במקרה של שגיאה
            }
        }


        public List<AdminCourse> UpdateAdminCourses(int id, AdminCourse updatedCourse)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@pCourseID", id);
                SqlParameter param2 = new SqlParameter("@pCourseName", updatedCourse.courses_name);
                SqlParameter param3 = new SqlParameter("@pTitle", updatedCourse.title);
                SqlParameter param4 = new SqlParameter("@pDescription", updatedCourse.description);
                SqlParameter param5 = new SqlParameter("@pUpdate_at", updatedCourse.update_at);
                SqlParameter param6 = new SqlParameter("@pPrice", updatedCourse.price);
                SqlParameter param7 = new SqlParameter("@pSeveral_chapters", updatedCourse.Several_chapters);
                SqlParameter param8 = new SqlParameter("@pLength", updatedCourse.Length);
                SqlParameter param9 = new SqlParameter("@pNumberOfViewers", updatedCourse.numberOfViewers);
                SqlParameter param11 = new SqlParameter("@pVideoURL", updatedCourse.videoURL);
                SqlParameter param12 = new SqlParameter("@pTaskFilesURL", updatedCourse.taskFilesURL);

                var courses = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("UpdateCourse_SP", param1, param2, param3, param4, param5, param6, param7, param8, param9, param11, param12);

                return courses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAdminCourses: {ex.Message}");
                return new List<AdminCourse>(); // החזרה של רשימה ריקה במקרה של שגיאה
            }
        }

        public List<AdminCourse> DeleteAdminCourse(int id)
        {
            SqlParameter param1 = new SqlParameter("@pCourseID", id);
            var t = SQLDataAccess.ExecuteStoredProcedure<List<AdminCourse>>("DeleteCourse_SP", param1);
            return t;
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
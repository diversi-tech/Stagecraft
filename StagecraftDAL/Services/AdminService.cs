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
            return _courses;
        }

        public AdminCourse GetAdminCourseById(int id)
        {
            return _courses.FirstOrDefault(c => c.courses_id == id);
        }

        public AdminCourse AddAdminCourses(AdminCourse course)
        {
            _courses.Add(course); 
            return course;
        }

        public AdminCourse UpdateAdminCourses(int id, AdminCourse updatedCourse)
        {
            var course = GetAdminCourseById(id);
            if (course == null)
            {
                return null;
            }
            course.courses_name = updatedCourse.courses_name;
            course.title = updatedCourse.title;
            course.description = updatedCourse.description;
            course.create_at = updatedCourse.create_at;
            course.update_at = updatedCourse.update_at;
            course.price = updatedCourse.price;
            course.Several_chapters = updatedCourse.Several_chapters;
            course.Length = updatedCourse.Length;
            course.numberOfViewers = updatedCourse.numberOfViewers;
            return course;
        }
        public List<Users> GetAllUsers()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Users>>("GetAllUsers", null);
            return t;
        }

        public bool AddCoursToUser(UserCourses userCourses)
        {
            SqlParameter param1 = new SqlParameter("@userId", userCourses.user_id);
            SqlParameter param2 = new SqlParameter("@coursId", userCourses.courses_id);
            SqlParameter param3 = new SqlParameter("@isApproved", userCourses.is_approved);
            var t = DataAccess.ExecuteStoredProcedure<bool>("AddCoursToUser", param1, param2, param3);
            return t;
        }

        public bool DeletCoursToUser(UserCourses userCourses)
        {
            SqlParameter param1 = new SqlParameter("@userId", userCourses.user_id);
            SqlParameter param2 = new SqlParameter("@coursId", userCourses.courses_id);
            var t = DataAccess.ExecuteStoredProcedure<bool>("DeletCoursToUser", param1, param2);
            return t;
        }

        public List<UserCourses> GetAllCoursOfUser(int userId)
        {
            SqlParameter param1 = new SqlParameter("@userId", userId);
            var t = DataAccess.ExecuteStoredProcedure<List<UserCourses>>("GetAllCoursOfUser", param1);
            return t;

        }
    }
}
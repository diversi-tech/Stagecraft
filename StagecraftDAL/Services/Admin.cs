using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using StagecraftDAL.Interface;


namespace StagecraftDAL.Services
{
    public class Admin: IAdmin
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace StagecraftDAL.Interface
{
    public interface IAdmin
    {
        List<AdminCourse> GetAllAdminCourses();
        AdminCourse GetAdminCourseById(int id);
        AdminCourse AddAdminCourses(AdminCourse course);
        AdminCourse UpdateAdminCourses(int id, AdminCourse updatedCourse);
        List<Users> GetAllUsers();
        bool AddCoursToUser(UserCourses userCourses);
        bool DeletCoursToUser(UserCourses userCourses);
       List<Course> GetAllCoursOfUser(int userId);

    }
}

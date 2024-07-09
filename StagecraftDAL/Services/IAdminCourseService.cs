using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace StagecraftDAL.Services
{
    public interface IAdminCourseService
    {
        List<AdminCourse> GetAllAdminCourses();
        List<AdminCourse> GetAdminCourseById(int id);
        List<AdminCourse> AddAdminCourses(AdminCourse course);
        List<AdminCourse> UpdateAdminCourses(int id, AdminCourse updatedCourse);
        List<AdminCourse> DeleteAdminCourse(int id);
    }
}

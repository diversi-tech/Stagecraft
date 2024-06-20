using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Services
{
    public class CourseService
    {
        public static IEnumerable<Course>  GetAllCourses()
        {
            Course course = new Course();
            var t = DataAccess<Course>.ExecuteStoredProcedure("getAllCourses", null);
            return t;
        }


    }
}

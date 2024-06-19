using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataAccess.ExecuteStoredProcedure;

namespace StagecraftDAL.Services
{
    internal class CourseService
    {
        public static Course GetAllCourses()
        {
            Course course = new Course();
            var t = DataAccess<Course>.ExecuteStoredProcedure("getAllCourses", null);
            return course;
        }


    }
}

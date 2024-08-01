using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface ICourse
    {
     
        List<Course> GetAllCourses();
        //warning!!!-
        List<Course> GetCourseDetails();
        Course GetCourseById(int courses_id);
       // List<Course> GetCoursesByUserId(int userId);
    }
}

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
        List<Course> GetCourseDetails();
        List<Course> GetCoursById(int courses_id);
    }
}

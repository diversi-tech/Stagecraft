using Common;
using System.Data.SqlClient;

namespace StagecraftDAL.Services
{
    public class CourseService
    {
        public static List<Course>  GetAllCourses()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Course>>("GetAllCourses", null);
            return t;
        }

        public static List<Course> GetCoursById(int courses_id)
        {
            SqlParameter param1 = new SqlParameter("@courses_id", courses_id);
            var t = DataAccess.ExecuteStoredProcedure<List<Course>>("getCoursById", param1);
            return t;
        }

       
        public static List<Course> GetCourseDetails()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Course>>("getAllCourses", null);
            return t;
        }


    }
}

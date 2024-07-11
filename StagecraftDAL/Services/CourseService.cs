using Common;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;
using System.Data;
using System.Data.SqlClient;

namespace StagecraftDAL.Services
{
    public class CourseService: ICourse
    {
        private readonly string _connectionString;

        public CourseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public static List<Classes> GetAllClass()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Classes>>("GetAllClass", null);
            return t;
        }
        public  List<Course>  GetAllCourses()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Course>>("GetAllCourses", null);
            return t;
        }

        public  List<Course> GetCourseById(int course_id)
        {
            SqlParameter param1 = new SqlParameter("@course_id", course_id);
            var t = DataAccess.ExecuteStoredProcedure<List<Course>>("getCourseById", param1);
            return t;
        }

       
        public  List<Course> GetCourseDetails()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Course>>("getAllCourses", null);
            return t;
        }

        List<Course> ICourse.GetCoursesByUserId(int userId)
        {
            List<Course> courses = new List<Course>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("GetCoursesByUserId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            courses_id = (int)reader["courses_id"],
                            courses_name = (string)reader["courses_name"],
                            title = (string)reader["title"],
                            description = (string)reader["description"],
                            Price = (int)reader["Price"],
                            numberOfViewers = (int)reader["numberOfViewers"]
                        });
                    }
                }
            }
            return courses;
        }


    }
}

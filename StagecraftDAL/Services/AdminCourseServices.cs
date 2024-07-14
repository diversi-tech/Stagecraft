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
   /* public class AdminCourseServices: IAdmin
    {
        private List<AdminCourse> _courses = new List<AdminCourse>();

        public List<AdminCourse> GetAllAdminCourses()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<AdminCourse>>("GetAllCourses", null);
            return t;
        }

        public List<AdminCourse> GetAdminCourseById(int id)
        {
            SqlParameter param1 = new SqlParameter("@CourseId", id);
            var t = DataAccess.ExecuteStoredProcedure<List<AdminCourse>>("GetCourseById", param1);
            return t;
        }

        public List<AdminCourse> AddAdminCourses(AdminCourse course)
        {
            SqlParameter param1 = new SqlParameter("@pCourseID", course.courses_id);
            SqlParameter param2 = new SqlParameter("@pCourseName", course.courses_name);
            SqlParameter param3 = new SqlParameter("@pTitle", course.title);
            SqlParameter param4 = new SqlParameter("@pDescription", course.description);
            SqlParameter param10 = new SqlParameter("@pCreate_at", course.create_at);
            SqlParameter param5 = new SqlParameter("@pUpdate_at", course.update_at);
            SqlParameter param6 = new SqlParameter("@pPrice", course.price);
            SqlParameter param7 = new SqlParameter("@pSeveral_chapters", course.Several_chapters);
            SqlParameter param8 = new SqlParameter("@pLength", course.Length);
            SqlParameter param9 = new SqlParameter("@pnumberOfViewers", course.numberOfViewers);

            var t = DataAccess.ExecuteStoredProcedure<List<AdminCourse>>("AddCourse_SP", param1, param2, param3, param4, param5, param6, param7, param8, param9,param10);
            Console.WriteLine(t);
            return t;
        }

        public List<AdminCourse> UpdateAdminCourses(int id, AdminCourse updatedCourse)
        {
            SqlParameter param1 = new SqlParameter("@pCourseID", id);
            SqlParameter param2 = new SqlParameter("@pCourseName", updatedCourse.courses_name);
            SqlParameter param3 = new SqlParameter("@pTitle", updatedCourse.title);
            SqlParameter param4 = new SqlParameter("@pDescription", updatedCourse.description);
            SqlParameter param5 = new SqlParameter("@pUpdate_at", updatedCourse.update_at);
            SqlParameter param6 = new SqlParameter("@pPrice", updatedCourse.price);
            SqlParameter param7 = new SqlParameter("@pSeveral_chapters", updatedCourse.Several_chapters);
            SqlParameter param8 = new SqlParameter("@pLength", updatedCourse.Length);
            SqlParameter param9 = new SqlParameter("@pnumberOfViewers", updatedCourse.numberOfViewers);
           
            var t = DataAccess.ExecuteStoredProcedure<List<AdminCourse>>("UpdateCourse_SP", param1, param2, param3, param4, param5, param6, param7, param8, param9);
            return t;
        }
        public List<AdminCourse> DeleteAdminCourse(int id)
        {
            SqlParameter param1 = new SqlParameter("@pCourseID", id);
            var t = DataAccess.ExecuteStoredProcedure<List<AdminCourse>>("DeleteCourse_SP", param1);
            return t;
        }
*/
  //  }
}
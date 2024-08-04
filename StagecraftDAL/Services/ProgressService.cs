using StagecraftDAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Services
{
    public class ProgressService : IProgressService
    {
        public int UpdateUserProgress(int userId, int courseId, int classId)
        {
            SqlParameter param1 = new SqlParameter("@UserID", userId);
            SqlParameter param2 = new SqlParameter("@CourseID", courseId);
            SqlParameter param3 = new SqlParameter("@ClassID", classId);

            try
            {
                var result = SQLDataAccess.ExecuteStoredProcedure<int>("UpdateUserProgress", param1, param2, param3);
                return result;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error updating user progress.", ex);
            }
        }
    }
}

using Npgsql;
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
            NpgsqlParameter param1 = new NpgsqlParameter("p_user_id", userId);
            NpgsqlParameter param2 = new NpgsqlParameter("p_course_id", courseId);
            NpgsqlParameter param3 = new NpgsqlParameter("p_class_id", classId);

            try
            {
                var result = PostgreSQLDataAccess.ExecuteSimpleTypeFunction<int>("update_user_progress", param1, param2, param3);
                return result;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error updating user progress.", ex);
            }
        }
    }
}

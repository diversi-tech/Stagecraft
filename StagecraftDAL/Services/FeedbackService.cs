using Common;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace StagecraftDAL.Services
{
    public class FeedbackService : IFeedback
    {
        private readonly string _connectionString;

        // Constructor to initialize connection string
        public FeedbackService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to get feedback by UserId and CourseId
        public List<Feedback> GetFeedbackByUserCourseClass(int UserId, int CourseId)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@CourseId", CourseId)
            };

            return DataAccess.ExecuteStoredProcedure<List<Feedback>>("GetFeedbackByUserCourseClass", parameters);
        }
    }
}
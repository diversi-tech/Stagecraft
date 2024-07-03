using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace StagecraftDAL.Services
{
    public class TranscriptSegmentService : ITranscriptSegmentService
    {
        private readonly string _connectionString;

        public TranscriptSegmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<TranscriptSegment> GetTranscriptionByVideoId(int videoId)
        {
            List<TranscriptSegment> segments = new List<TranscriptSegment>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("GetTranscriptionByVideoId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@VideoId", videoId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        segments.Add(new TranscriptSegment
                        {
                            Id = (int)reader["Id"],
                            VideoId = (int)reader["VideoId"],
                            Time = (int)reader["Time"],
                            Text = (string)reader["Text"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = (DateTime)reader["UpdatedAt"]
                        });
                    }
                }
            }
            return segments;
        }

    }
}

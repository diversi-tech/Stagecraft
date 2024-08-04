using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Interface;
using Npgsql;

namespace StagecraftDAL.Services
{
    public class VideoService : IVideoService
    {
        private readonly string _connectionString;

        public VideoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Videos GetVideoById(int videoId)
        {

            NpgsqlParameter param1 = new NpgsqlParameter("pvideo_id", videoId);
            var t = PostgreSQLDataAccess.ExecuteFunction<Videos>("get_video_by_id", param1).FirstOrDefault();

            return t;
            //Videos video = null;
            //using (var connection = new SqlConnection(_connectionString))
            //using (var command = new SqlCommand("GetVideoById", connection))
            //{
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@VideoId", videoId);

            //    connection.Open();
            //    using (var reader = command.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //            video = new Videos
            //            {
            //                video_id = (int)reader["video_id"],
            //                courses_id = (int)reader["courses_id"],
            //                video_name = (string)reader["video_name"],
            //                video_length = (int)reader["video_length"]
            //            };
            //        }
            //    }
            //}
            //return video;
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

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
using Npgsql;

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
            NpgsqlParameter param1 = new NpgsqlParameter("pvideo_id", videoId);
            var segments = PostgreSQLDataAccess.ExecuteFunction<TranscriptSegment>("get_transcription_by_video_id", param1);
            return segments;
        }

    }
}

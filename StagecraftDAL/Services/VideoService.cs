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
        public Videos GetVideoById(int videoId)
        {
            NpgsqlParameter param1 = new NpgsqlParameter("pvideo_id", videoId);
            var t = PostgreSQLDataAccess.ExecuteFunction<Videos>("get_video_by_id", param1).FirstOrDefault();

            return t;
        }

        public List<TranscriptSegment> GetTranscriptionByVideoId(int videoId)
        {
            NpgsqlParameter param1 = new NpgsqlParameter("pvideo_id", videoId);
            var segments = PostgreSQLDataAccess.ExecuteFunction<TranscriptSegment>("get_transcription_by_video_id", param1);
            return segments;
        }
    }
}

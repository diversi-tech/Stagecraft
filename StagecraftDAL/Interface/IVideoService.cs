using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IVideoService
    {
        Videos GetVideoById(int videoId);
        List<TranscriptSegment> GetTranscriptionByVideoId(int videoId);
    }
}

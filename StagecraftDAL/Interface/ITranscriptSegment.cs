using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace StagecraftDAL.Interface
{
    public interface ITranscriptSegmentService
    {
        List<TranscriptSegment> GetTranscriptionByVideoId(int videoId);
    }
}

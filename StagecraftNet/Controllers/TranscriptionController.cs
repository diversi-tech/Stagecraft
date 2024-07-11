using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranscriptionController : ControllerBase
    {
        private readonly ITranscriptSegmentService _transcriptSegmentService;

        public TranscriptionController(ITranscriptSegmentService transcriptSegmentService)
        {
            _transcriptSegmentService = transcriptSegmentService;
        }

        [HttpGet("transcription/{videoId}")]
        public IActionResult GetTranscription(int videoId)
        {
            var transcriptSegments = _transcriptSegmentService.GetTranscriptionByVideoId(videoId);
            return Ok(transcriptSegments);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;

namespace StagecraftNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpPost("UpdateUserProgress")]
        public ActionResult UpdateUserProgress([FromBody] UpdateProgressRequest request)
        {
            try
            {
                var result = _progressService.UpdateUserProgress(request.UserId, request.CourseId, request.ClassId);
                if (result > 0)
                {
                    return Ok(new { Success = true });
                }
                return BadRequest(new { Success = false, Message = "Update failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }
    }

    public class UpdateProgressRequest
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int ClassId { get; set; }
    }
}

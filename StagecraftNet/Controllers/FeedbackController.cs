using Common;
using Microsoft.AspNetCore.Mvc;

using StagecraftDAL.Interface;
using StagecraftDAL.Services;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedback _feedbackService;

        public FeedbackController(IFeedback feedbackService)
        {
            _feedbackService = feedbackService;
        }





        [HttpGet]
        [Route("GetFeedbackByUserCourseClass/{UserId}/{CourseId}")]
        public IActionResult GetFeedbackByUserCourseClass(int UserId, int CourseId)
        {
            List<Feedback> feedback = _feedbackService.GetFeedbackByUserCourseClass(UserId, CourseId);

            if (feedback == null || feedback.Count == 0)
            {
                return NotFound("Feedback not found");
            }

            return Ok(feedback);
        }
    }
}

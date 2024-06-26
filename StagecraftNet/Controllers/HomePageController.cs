using Microsoft.AspNetCore.Mvc;
using Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StagecraftDAL.Services;
namespace StagecraftNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomePageController : ControllerBase
    {

        private readonly ILogger<HomePageController> _logger;
        public HomePageController(ILogger<HomePageController> logger)
        {
            _logger = logger;
            var t = 0;
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public ActionResult GetAllCourses()
        {
            // ����� �������� GetAllCourses ��-CourseService
            try
            {
                var t = CourseService.GetAllCourses();
                return Ok(t);
            }
            // ����� ������ ������ �� ����� ������� ������

            catch (Exception ex)
            {
                // ���� ������ ������ ���
                return Ok(ex);
            }
        }
        [HttpGet()]
        [Route("CheckIfEmailExists/{email}")]
        public ActionResult CheckIfEmailExists(string email)
        {
            // ����� �������� GetAllCourses ��-CourseService
            try
            {
                var t = UsersService.CheckIfEmailExists(email);
                return Ok(t);
            }
            // ����� ������ ������ �� ����� ������� ������

            catch (Exception ex)
            {
                // ���� ������ ������ ���
                return Ok(ex);
            }
        }
        [HttpGet()]
        [Route("GETCOURSEBYID/{id}")]
        public ActionResult GETCOURSEBYID(int id)
        {
            // ����� �������� GetAllCourses ��-CourseService
            try
            {
                var t = CourseService.GETCOURSEBYID(id);
                return Ok(t);
            }
            // ����� ������ ������ �� ����� ������� ������

            catch (Exception ex)
            {
                // ���� ������ ������ ���
                return Ok(ex);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetCourseDdetails()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public void GetAvailableCourse()

        {


            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] Users userDetails)
        {
            throw new NotImplementedException();
        }
        [HttpPost("{id}")]
        public IActionResult SignUpForACourse([FromQuery] int courseId)
        {
            throw new NotImplementedException();
        }


    }
}

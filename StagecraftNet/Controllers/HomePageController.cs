using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Services;
using StagecraftDAL.Interface;
using StagecraftApi.JwtManager;
//using System.Web.Mvc;
//using Microsoft.AspNetCore.Mvc;
namespace StagecraftNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomePageController : ControllerBase
    {

      
        private readonly ILogger<HomePageController> _logger;
        private readonly ICourse _Course;


        public HomePageController(ILogger<HomePageController> logger, ICourse course)
        {
            _Course = course;
            _logger = logger;
            var t = 0;
        }

        [HttpGet]
        //[StagecraftApi.JwtManager.Authorize(Roles.User)]

        [Route("GetAllClass")]
        public ActionResult GetAllClass()
        {
            // ����� �������� GetAllClass ��-CourseService
            try
            {
                var t = CourseService.GetAllClass();
                return Ok(t);
            }
            // ����� ������ ������ �� ����� ������ ������

            catch (Exception ex)
            {
                // ���� ������ ������ ���
                return Ok(ex);
            }
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public ActionResult GetAllCourses()
        {
            // ����� �������� GetAllCourses ��-CourseService
            try
            {
                var t = _Course.GetAllCourses();
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
        [Route("GetCourseById/{id}")]
        public ActionResult GetCourseById(int id)
        {
            try
            {
                var t = _Course.GetCourseById(id);
                return Ok(t);

            }
            catch (Exception ex)
            {
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

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] Users user)
        {
            try
            {
                UsersService.SignUp(user);
                return Ok("User signed up successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //[HttpPost("{id}")]
        [HttpPost("signup-course")]
        public IActionResult SignUpForACourse([FromBody] int courseId)
        {
            try
            {
                UsersService.SignUpForACourse(courseId);
                return Ok("Signed up for course successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}

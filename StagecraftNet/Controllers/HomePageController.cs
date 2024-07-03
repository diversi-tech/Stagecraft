using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Services;
using StagecraftDAL.Interface;
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
        [Route("GetAllCourses")]
        public ActionResult GetAllCourses()
        {
            // קריאה לפונקציה GetAllCourses שב-CourseService
            try
            {
                var t = _Course.GetAllCourses();
                return Ok(t);
            }
            // לשלוף נתונים ולשלוף את רשימת הקורסים לתצוגה

            catch (Exception ex)
            {
                // הוסף לוגיקה ללוגים כאן
                return Ok(ex);
            }
        }
        
        [HttpGet()]
        [Route("GetCoursById/{id}")]
        public ActionResult GetCoursById(int id)
        {
            try
            {
                var t = _Course.GetCoursById(id);
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

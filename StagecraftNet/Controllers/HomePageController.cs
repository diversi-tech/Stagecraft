using Microsoft.AspNetCore.Mvc;
using Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StagecraftDAL.Services;
//using System.Web.Mvc;
//using Microsoft.AspNetCore.Mvc;
namespace StagecraftNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomePageController : ControllerBase
    {

        //    private readonly CourseService _courseService;

        //public HomePageController()
        //{
        //    _courseService = new CourseService(); // Initialize your CourseService instance
        //}
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    // ����� �������� GetAllCourses ��-CourseService
        //    try
        //    {
        //        return Ok(CourseService.GetAllCourses());
        //    }
        //    // ����� ������ ������ �� ����� ������� ������

        //    catch (Exception ex)
        //    {
        //        // ���� ������ ������ ���
        //        return Ok(ex);
        //    }
        //}
        //public ActionResult Index()
        //{
        //    // Assuming GetAllCourses() returns IEnumerable<Course> or similar
        //    IEnumerable<Course> courses = _courseService.GetAllCourses();

        //    // Pass the collection of courses to the view
        //    return View(courses);
        //}
        private readonly ILogger<HomePageController> _logger;

        public HomePageController(ILogger<HomePageController> logger )
        {
            _logger = logger;

            var t = 0;
        }
      

        [HttpGet("{id}")]

        public  IActionResult GetCourseDdetails()
        {
            throw new NotImplementedException();    
        }
        [HttpGet]
        public  void GetAvailableCourse()

        {


            throw new NotImplementedException();
        }

        [HttpPost]
        public  IActionResult SignUp([FromBody] Users userDetails) 
        {
            throw new NotImplementedException();
        }
        [HttpPost("{id}")]
        public  IActionResult SignUpForACourse([FromQuery] int courseId)
        {
            throw new NotImplementedException();
        }


    }
}

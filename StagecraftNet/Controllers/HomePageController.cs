using Microsoft.AspNetCore.Mvc;

using Common;

namespace StagecraftNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomePageController : ControllerBase
    {

        private readonly ILogger<HomePageController> _logger;

        public HomePageController(ILogger<HomePageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]

        public static IActionResult GetCourseDdetails()
        {
            throw new NotImplementedException();    
        }
        [HttpGet]
        public static IActionResult GetAvailableCourse()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public static IActionResult SignUp([FromQuery] UserDetails userDetails) 
        {
            throw new NotImplementedException();
        }
        [HttpPost("{id}")]
        public static IActionResult SignUpForACourse([FromQuery] int courseId)
        {
            throw new NotImplementedException();
        }


    }
}

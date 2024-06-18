using Microsoft.AspNetCore.Mvc;
using Common;
using Microsoft.Extensions.Options;


namespace StagecraftNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomePageController : ControllerBase
    {
       

        private readonly ILogger<HomePageController> _logger;

        public HomePageController(ILogger<HomePageController> logger )
        {
            _logger = logger;
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
        public  IActionResult SignUp([FromBody] UserDetails userDetails) 
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

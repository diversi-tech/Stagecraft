using Microsoft.AspNetCore.Mvc;
using Common;
using Microsoft.Extensions.Options;


namespace StagecraftNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class loginController : ControllerBase
    {

        private readonly ILogger<HomePageController> _logger;
        private readonly List<loginController> users = new List<loginController>

        public loginController(ILogger<HomePageController> logger )
        {
            _logger = logger;
            var t = 0;
        }


        [HttpPost("login")]

        public  IActionResult Login([FromBody] UserLoginRequest request)
        {
            var user = users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user != null)
            {
                return Ok(new { message = "משתמש נמצא במערכת" });
            }
             else
            {
                return NotFound(new { message = "משתמש לא נמצא במערכת" });
            }
            throw new NotImplementedException();    
        }
       


    }
}

using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;
using StagecraftDAL.Services;


namespace StagecraftNet.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
     [ApiController]
     [Route("api/[controller]")]
    public class loginController : ControllerBase
    {

            private readonly ILogin _loginService;

            public loginController(ILogin loginService)
            {
            _loginService = loginService;
            }

            [HttpPost("check-existence")]
            public async Task<IActionResult> CheckUserExistence([FromBody] Users user)
            {
                var userExists = await _loginService.CheckUserExistence(user);
                return Ok(new { Result = userExists });
            }
        }

    }


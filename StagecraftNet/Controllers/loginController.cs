using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;


namespace StagecraftNet.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {

        private readonly ILogin _loginService;

        public loginController(ILogin loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("CheckUserExistence")]
        public async Task<ActionResult<bool>> CheckUserExistence([FromBody] Users credentials)
        {
            var userExists = await _loginService.CheckUserExistence(credentials);

            return Ok(userExists);
        }
    }

}


using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;
using System.Net.Http;


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
        public ActionResult CheckUserExistence([FromBody] Users credentials)
        {
            try
            {


                var userExists = _loginService.CheckUserExistence(credentials);

                return Ok(userExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}




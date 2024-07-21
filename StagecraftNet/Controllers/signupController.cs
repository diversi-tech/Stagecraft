using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;

namespace StagecraftNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignup _signupService;

        public SignupController(ISignup signupService)
        {
            _signupService = signupService;
        }

        [HttpPost("RegisterUser")]
        public ActionResult RegisterUser([FromBody] Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User is null.");
                }

                var userId = _signupService.RegisterUser(user);
                return Ok(userId);
            }
            catch (InvalidOperationException ex) when (ex.Message == "Email already exists.")
            {
                return BadRequest("Email already exists.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

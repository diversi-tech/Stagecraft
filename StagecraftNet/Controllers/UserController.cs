

using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Services;
using StagecraftNet.Controllers;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserDAL _userDAL;

        public UserController(IUserDAL UsersService)
        {
            _userDAL = UsersService;
        }

        [HttpGet()]
        [Route("GetUserProgress/{userId}/{courseId}")]
        public  IActionResult GetUserProgress(int userId, int courseId)
        {
            var progress =  _userDAL.GetUserProgress(userId, courseId);
            if (progress == null)
            {
                return NotFound();
            }

            return Ok(progress);
        }
    }
}

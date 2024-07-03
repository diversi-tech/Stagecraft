

using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;
using StagecraftNet.Controllers;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUser _userDAL;

        public UserController(IUser UsersService)
        {
            _userDAL = UsersService;
        }

        [HttpGet("{userId}/{courseId}")]
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

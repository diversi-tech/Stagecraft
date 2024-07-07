

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
        public IActionResult GetUserProgress(int userId, int courseId)
        {
            try
            {
                var progress = _userDAL.GetUserProgress(userId, courseId);
                return Ok(progress);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}

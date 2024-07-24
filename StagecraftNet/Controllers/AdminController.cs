using System.Data.SqlClient;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StagecraftApi.JwtManager;
using StagecraftDAL.Interface;
using StagecraftDAL.Services;

namespace StagecraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

       // private readonly IAdminCourseService _adminCourseService;
        private readonly IAdmin _admin;

        public AdminController(IAdmin adminCourseService)
        {
            _admin = adminCourseService;
        }

        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        // Get all courses
        [HttpGet("GetAllAdminCourses")]
        public ActionResult<IEnumerable<AdminCourse>> GetAllAdminCourses()
        {
            var courses = _admin.GetAllAdminCourses();
            return Ok(courses);
        }
        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        // Get a course by ID
        [HttpGet("GetAdminCourseById/{id}")]
        public ActionResult<AdminCourse> GetAdminCourseById(int id)
        {
            var course = _admin.GetAdminCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        // Add a new course
        [HttpPost("AddAdminCourses")]
        public ActionResult<AdminCourse> AddAdminCourses([FromBody] AdminCourse course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCourse = _admin.AddAdminCourses(course);
            return CreatedAtAction(nameof(AddAdminCourses), new { id = course.courses_id }, course);

        }
        // [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        // Update an existing course
        [HttpPut("UpdateAdminCourses/{id}")]
        public IActionResult UpdateAdminCourses(int id, [FromBody] AdminCourse course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedCourse = _admin.UpdateAdminCourses(id, course);
            if (updatedCourse == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpDelete("DeleteAdminCourse/{id}")]
        public IActionResult DeleteAdminCourse(int id)
        {
            var deletedCourse = _admin.DeleteAdminCourse(id);
            if (deletedCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return Ok(deletedCourse);
        }
        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpGet()]
        [Route("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var t = _admin.GetAllUsers();
                return Ok(t);
            }

            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpGet()]
        [Route("GetAllCoursOfUser/{userId}")]
        public ActionResult GetAllCoursOfUser(int userId)
        {
            try
            {
                var t = _admin.GetAllCoursOfUser(userId);
                return Ok(t);
            }

            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //[StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpPost("AddCoursToUser")]
        public ActionResult AddCoursToUser([FromBody] UserCourses userCourses)
        {
            if (userCourses == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var t = _admin.AddCoursToUser(userCourses);
                    return Ok(t);
                }

                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }

        }

        // [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpDelete("DeletCoursToUser")]
        public ActionResult DeletCoursToUser([FromBody] UserCourses userCourses)
        {
            if (userCourses == null)
                return BadRequest();
            try
            {
                var t = _admin.DeletCoursToUser(userCourses);
                return Ok(t);
            }

            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
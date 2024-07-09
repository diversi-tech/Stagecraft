using System.Data.SqlClient;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Services;

namespace StagecraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminCourseService _adminCourseService;

        public AdminController(IAdminCourseService adminCourseService)
        {
            _adminCourseService = adminCourseService;
        }

        // Get all courses
        [HttpGet("GetAllAdminCourses")]
        public ActionResult<IEnumerable<AdminCourse>> GetAllAdminCourses()
        {
            var courses = _adminCourseService.GetAllAdminCourses();
            return Ok(courses);
        }

        // Get a course by ID
        [HttpGet("GetAdminCourseById/{id}")]
        public ActionResult<AdminCourse> GetAdminCourseById(int id)
        {
            var course = _adminCourseService.GetAdminCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        // Add a new course
        [HttpPost("AddAdminCourses")]
        public ActionResult<AdminCourse> AddAdminCourses([FromBody] AdminCourse course) 
        {
            if (!ModelState.IsValid) 
                {
                return BadRequest(ModelState);
                }
            var newCourse = _adminCourseService.AddAdminCourses(course);
            return CreatedAtAction(nameof(AddAdminCourses), new { id = course.courses_id }, course);
           
        }

        // Update an existing course
        [HttpPut("UpdateAdminCourses/{id}")]
        public IActionResult UpdateAdminCourses(int id, [FromBody] AdminCourse course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedCourse = _adminCourseService.UpdateAdminCourses(id, course);
            if (updatedCourse == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("DeleteAdminCourse/{id}")]
        public IActionResult DeleteAdminCourse(int id)
        {
            var deletedCourse = _adminCourseService.DeleteAdminCourse(id);
            if (deletedCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return Ok(deletedCourse);
        }

    }
}
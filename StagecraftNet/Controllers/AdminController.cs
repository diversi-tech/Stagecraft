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
        [HttpGet("{id}")]
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
        [HttpPost]
        public ActionResult<AdminCourse> AddAdminCourses([FromBody] AdminCourse course)
        {
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
            var newCourse = _adminCourseService.AddAdminCourses(course);
            return CreatedAtAction(nameof(GetAdminCourseById), new { id = newCourse.courses_id }, newCourse);
        }

        // Update an existing course
        [HttpPut("{id}")]
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
    }
}
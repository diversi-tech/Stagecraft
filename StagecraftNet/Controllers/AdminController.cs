using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;

namespace StagecraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin adminCourseService)
        {
            _admin = adminCourseService;
        }

        // Get all courses
        [HttpGet("GetAllAdminCourses")]
        public ActionResult<IEnumerable<AdminCourse>> GetAllAdminCourses()
        {
            var courses = _admin.GetAllAdminCourses();
            return Ok(courses);
        }

        // Get a course by ID
        [HttpGet("{id}")]
        public ActionResult<AdminCourse> GetAdminCourseById(int id)
        {
            var course = _admin.GetAdminCourseById(id);
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
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            var newCourse = _admin.AddAdminCourses(course);
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
            var updatedCourse = _admin.UpdateAdminCourses(id, course);
            if (updatedCourse == null)
            {
                return NotFound();
            }
            return NoContent();
        }
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
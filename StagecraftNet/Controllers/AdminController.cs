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

        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        // Get all courses
        [HttpGet("GetAllAdminCourses")]
        public ActionResult<IEnumerable<AdminCourse>> GetAllAdminCourses()
        {
            var courses = _admin.GetAllAdminCourses();
            return Ok(courses);
        }
        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

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
        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        // Add a new course
        [HttpPost("AddAdminCourses")]
        public async Task<ActionResult<AdminCourse>> AddAdminCourses([FromForm] AdminCourse course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (course.videoFile != null)
            {
                course.videoURL = await StagecraftBL.FilesBL.SaveFileAsync(course.videoFile, FileSaveStatus.Add);
            }

            if (course.taskFile != null)
            {
                course.taskFilesURL = await StagecraftBL.FilesBL.SaveFileAsync(course.taskFile, FileSaveStatus.Add);
            }

            var newCourse = _admin.AddAdminCourses(course);

            return CreatedAtAction(nameof(AddAdminCourses), new { id = course.courses_id }, course);
        }
        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpPut("UpdateAdminCourses/{id}")]
        public async Task<IActionResult> UpdateAdminCourses(int id, [FromForm] AdminCourse course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourse = _admin.GetAdminCourseById(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            // בדיקה אם יש קובץ וידאו חדש
            if (course.videoFile != null)
            {
                if (!string.IsNullOrEmpty(existingCourse.videoURL))
                {
                    // מחיקת קובץ וידאו ישן
                    await StagecraftBL.FilesBL.SaveFileAsync(null, FileSaveStatus.Delete, existingCourse.videoURL);
                }
                // שמירת קובץ וידאו חדש
                course.videoURL = await StagecraftBL.FilesBL.SaveFileAsync(course.videoFile, FileSaveStatus.Update);
            }
            else
            {
                // שמירת קובץ וידאו ישן אם אין עדכון
                course.videoURL = existingCourse.videoURL;
            }

            // בדיקה אם יש קובץ משימה חדש
            if (course.taskFile != null)
            {
                if (!string.IsNullOrEmpty(existingCourse.taskFilesURL))
                {
                    // מחיקת קובץ משימה ישן
                    await StagecraftBL.FilesBL.SaveFileAsync(null, FileSaveStatus.Delete, existingCourse.taskFilesURL);
                }
                // שמירת קובץ משימה חדש
                course.taskFilesURL = await StagecraftBL.FilesBL.SaveFileAsync(course.taskFile, FileSaveStatus.Update);
            }
            else
            {
                // שמירת קובץ משימה ישן אם אין עדכון
                course.taskFilesURL = existingCourse.taskFilesURL;
            }

            // עדכון פרטי הקורס
            var updatedCourse = _admin.UpdateAdminCourses(id, course);
            if (updatedCourse == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

        [HttpDelete("DeleteAdminCourse/{id}")]
        public async Task<IActionResult> DeleteAdminCourse(int id)
        {
            var course = _admin.GetAdminCourseById(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            try
            {
                // מחיקת קבצים מקושרים אם קיימים
                if (!string.IsNullOrEmpty(course.videoURL))
                {
                    await StagecraftBL.FilesBL.SaveFileAsync(null, FileSaveStatus.Delete, course.videoURL);
                }

                if (!string.IsNullOrEmpty(course.taskFilesURL))
                {
                    await StagecraftBL.FilesBL.SaveFileAsync(null, FileSaveStatus.Delete, course.taskFilesURL);
                }

                // מחיקת הקורס מהמאגר
                var deletedCourse = _admin.DeleteAdminCourse(id);
                if (deletedCourse == null)
                {
                    return NotFound($"Course with ID {id} not found.");
                }

                return Ok(new { message = "Course deleted successfully.", course = deletedCourse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the course.", error = ex.Message });
            }
        }

        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

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
        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

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
        [StagecraftApi.JwtManager.Authorize(Roles.Admin)]

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
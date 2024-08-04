using Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;
using StagecraftDAL.Services;
using StagecraftNet.Controllers;
namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourse _courseService;
        public CoursesController(ICourse CourseService)
        {
            _courseService = CourseService;
        }
        //[HttpGet]
        //[Route("GetCoursesByUserId/{userId}")]
        //public IActionResult GetCoursesByUserId(int userId)
        //{

        //    var courses = _courseService.GetCoursesByUserId(userId);
        //    if (courses == null || !courses.Any())
        //    {
        //        return NotFound("No courses found for the user.");
        //    }
        //    return Ok(courses);
        //}

        [HttpGet]

        [Route("GetCourseDetails")]
        public IActionResult GetCourseDetails()
        {
            var courseDetails = _courseService.GetAllCourses();
            if (courseDetails == null)
            {
                return NotFound("Course not found.");
            }
            return Ok(courseDetails);
        }

        [HttpGet]
        [Route("GetCourseById/{courses_id}")]
        public IActionResult GetCourseById(int courses_id)
        {
            var course = _courseService.GetCourseById(courses_id);
            if (course == null)
            {
                return NotFound("Course not found.");
            }
            return Ok(course);
        }



    }

}

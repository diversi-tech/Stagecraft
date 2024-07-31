using Microsoft.AspNetCore.Mvc;
using StagecraftApi.JwtManager;
using StagecraftDAL.Interface;
using StagecraftNet.Controllers;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
     


        private readonly IFile _fileDAL;

        public FilesController(IFile fileDAL)
        {
            _fileDAL = fileDAL;

        }

        [StagecraftApi.JwtManager.Authorize(Roles.User)]

        [HttpGet("{VideoId}")]
        public IActionResult DownloadTaskFiles(int VideoId)
        {
            try
            {
                // לוג לבדיקת הגעת הבקשה לפונקציה
                Console.WriteLine($"DownloadTaskFiles called with VideoId: {VideoId}");
                
                var videoPath = _fileDAL.DownloadTaskFiles(VideoId);

                // לוג לבדיקת הנתיב שהוחזר מהפורצדורה
                Console.WriteLine($"Path returned from DownloadTaskFiles: {videoPath}");
               
                if (System.IO.File.Exists(videoPath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(videoPath);
                    var fileName = Path.GetFileName(videoPath);
                    var mimeType = "application/octet-stream"; // שונה ל-application/pdf

                    return File(fileBytes, mimeType, fileName);
                }
                else
                {
                    // לוג במידה והקובץ לא נמצא
                    Console.WriteLine($"File not found at path: {videoPath}");

                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                // לוג לטיפול בשגיאות
                Console.WriteLine($"Error in DownloadTaskFiles: {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }
    }
}

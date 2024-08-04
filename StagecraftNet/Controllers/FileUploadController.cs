using Common;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using StagecraftDAL;
using System.Data.SqlClient;
using StagecraftDAL.Services;
using StagecraftDAL.Interface;
using StagecraftApi.JwtManager;

namespace StagecraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUpload _fileUploadService;

        public FileUploadController(IFileUpload fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }
        [StagecraftApi.JwtManager.Authorize(Roles.User)]

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var result = await _fileUploadService.UploadFileAsync(stream.ToArray(), file.FileName);
                if (result)
                    return Ok("File uploaded successfully.");
                else
                    return StatusCode(500, "Error uploading file.");
            }
        }
    }
}
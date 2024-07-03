﻿using Common;
using Microsoft.AspNetCore.Mvc;
using StagecraftDAL;
using StagecraftDAL.Interface;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetVideo(int id)
        {
            var video = _videoService.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }
    }
}
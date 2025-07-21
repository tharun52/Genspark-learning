using Microsoft.AspNetCore.Mvc;
using VideoManager.Services;
using VideoManager.Models;
using VideoManager.Interfaces;

namespace VideoManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingVideosController : ControllerBase
    {
        private readonly IFileService _fileService;

        public TrainingVideosController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            try
            {
                var videos = await _fileService.GetTrainingVideos();
                return Ok(videos);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoById(Guid id)
        {
            try
            {
                var video = await _fileService.GetTrainingVideoById(id);
                return Ok(video);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/blob-url")]
        public async Task<IActionResult> GetBlobUrl(Guid id)
        {
            try
            {
                var url = await _fileService.GetBlobUrl(id);
                return Ok(new { BlobUrl = url });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(Guid id)
        {
            try
            {
                await _fileService.DeleteTrainingVideo(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("upload")]
        [RequestSizeLimit(1_000_000_000)] 
        public async Task<IActionResult> UploadVideo([FromForm] UploadVideoDto videoDto)
        {
            try
            {
                var video = await _fileService.UploadTrainingVideo(videoDto);
                return CreatedAtAction(nameof(GetVideoById), new { id = video.Id }, video);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

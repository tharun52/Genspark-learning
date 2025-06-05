using FirstApi.Interfaces;
using FirstApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<byte[]>> GetFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("File path cannot be null or empty.");
            }
            var result = await _fileService.GetBytesAsync(filePath);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("File not found or content is null.");
        }

    
        [HttpPost]
        public async Task<ActionResult<byte[]>> PostFile([FromForm] FileRequestDto fileDto)
        {
            if (fileDto.File == null || fileDto.File.Length == 0)
            {
                return BadRequest("File cannot be null or empty.");
            }

            try
            {
                var result = await _fileService.PostFile(fileDto.File, fileDto.FileFormat);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
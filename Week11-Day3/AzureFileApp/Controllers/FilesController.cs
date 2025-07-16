using AzureFileApp.Interface;
using AzureFileApp.Models;
using BlobAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IBlobStorageService _blobStorageService;

        public FilesController(IBlobStorageService blobStorageService)
        {
            _blobStorageService  = blobStorageService;
        }
        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var stream = await _blobStorageService.DownloadFile(fileName);
            if (stream == null) 
                return NotFound();
            return File(stream, "application/octet-stream", fileName);
        }

        [Consumes("multipart/form-data")]
        
        [HttpPost("upload")]

        public async Task<IActionResult> Upload([FromForm] FileUploadDto request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file to upload");
            using var stream = request.File.OpenReadStream();
            await _blobStorageService.UploadFile(request);
            return Ok("File uploaded");
        }
    }
}
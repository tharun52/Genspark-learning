

namespace FirstApi.Models.DTOs
{
    public class FileRequestDto
    {
        public IFormFile? File { get; set; }
        public string? FileFormat { get; set; }
    }
}
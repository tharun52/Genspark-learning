using FirstApi.Models.DTOs;

namespace FirstApi.Interfaces
{
    public interface IFileService
    {
        public Task<byte[]?> GetBytesAsync(string filePath);
        public Task<string> PostFile(IFormFile fileData, string fileType);
    }
}
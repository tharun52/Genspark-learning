using System.IO;
using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs;

namespace FirstApi.Services
{
    public class FileService : IFileService
    {
        private readonly IRepository<int, Files> _fileRepository;

        public FileService(IRepository<int, Files> fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<byte[]?> GetBytesAsync(string filePath)
        {
            var files = await _fileRepository.GetAll();
            var file = files.FirstOrDefault(f => f.FilePath == filePath);
            if (file == null || file.Content == null)
            {
                return null;
            }
            return file.Content;
        }
        public async Task<string> PostFile(IFormFile fileData, string fileType)
        {
            try
            {
                var file = new Files
                {
                    FilePath = fileData.FileName,
                    FileFormat = fileType
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    file.Content = stream.ToArray();
                }

                var result = await _fileRepository.Add(file);
                return "File saved"; 
            }
            catch (System.Exception)
            {
                throw new Exception("Error saving file");
            }
        }
    }
}

using AzureFileApp.Models;

namespace AzureFileApp.Interface
{
    public interface IBlobStorageService
    {
        public Task UploadFile(FileUploadDto fileDto);
        public Task<Stream> DownloadFile(string fileName);
    }
}
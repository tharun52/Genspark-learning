using Azure.Storage.Blobs;
using AzureFileApp.Interface;
using AzureFileApp.Models;

namespace BlobAPI.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        public BlobStorageService(IConfiguration configuration)
        {
            var sasUrl = configuration["AzureBlob:ContainerSasUrl"];
            if (sasUrl == null)
            {
                throw new Exception("No url found");
            }
            _containerClient = new BlobContainerClient(new Uri(sasUrl));
        }

        public async Task UploadFile(FileUploadDto fileDto)
        {
            if (fileDto == null || fileDto.File == null)
            {
                throw new Exception("Invalid File Upload");
            }

            var blobClient = _containerClient.GetBlobClient(fileDto.File.FileName);

            using (var stream = fileDto.File.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var blobClient = _containerClient?.GetBlobClient(fileName);
            if (blobClient == null)
            {
                throw new Exception("Error in creating blobClient");
            }
            if (await blobClient.ExistsAsync())
                {
                    var downloadInfor = await blobClient.DownloadStreamingAsync();
                    return downloadInfor.Value.Content;
                }
            return null;
        }
    }
}
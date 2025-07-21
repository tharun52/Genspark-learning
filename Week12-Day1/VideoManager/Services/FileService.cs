using VideoManager.Interfaces;
using VideoManager.Models;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace VideoManager.Services
{
    public class FileService : IFileService
    {
        private readonly IRepository<Guid, TrainingVideo> _trainingVideoRepository;
        private readonly IConfiguration _configuration;

        public FileService(IRepository<Guid, TrainingVideo> trainingVideoRepository, IConfiguration configuration)
        {
            _trainingVideoRepository = trainingVideoRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<TrainingVideo>> GetTrainingVideos()
        {
            var videos = await _trainingVideoRepository.GetAll();
            if (videos == null || !videos.Any())
            {
                throw new Exception("No Videos Present in the Database");
            }
            return videos;
        }

        public async Task<TrainingVideo> GetTrainingVideoById(Guid videoId)
        {
            var video = await _trainingVideoRepository.Get(videoId);
            if (video == null)
            {
                throw new Exception("No video found by the given Id");
            }
            return video;
        }

        public async Task<string> GetBlobUrl(Guid videoId)
        {
            var video = await _trainingVideoRepository.Get(videoId);

            if (video == null || string.IsNullOrWhiteSpace(video.BlobUrl))
            {
                throw new Exception("Video not found or blob URL is missing");
            }

            return video.BlobUrl;
        }

        public async Task DeleteTrainingVideo(Guid id)
        {
            var video = await _trainingVideoRepository.Get(id);
            if (video == null)
            {
                throw new Exception("Video not found");
            }
            if (video.BlobUrl != null)
            {              
                var blobUrl = new Uri(video.BlobUrl);
                var containerClient = new BlobContainerClient(new Uri(_configuration["BlobContainerSas"]));
                var blobName = Path.GetFileName(blobUrl.LocalPath);
                var blobClient = containerClient.GetBlobClient(blobName);
                await blobClient.DeleteIfExistsAsync();
            }

            await _trainingVideoRepository.Delete(id);
        }

        public async Task<TrainingVideo> UploadTrainingVideo(UploadVideoDto videoDto)
        {
            if (videoDto.File == null || videoDto.File.Length == 0)
            {
                throw new ArgumentException("No file uploaded");
            }

            var allowedExtension = ".mp4";
            var fileExtension = Path.GetExtension(videoDto.File.FileName);

            if (!string.Equals(fileExtension, allowedExtension, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Only .mp4 files are allowed");
            }

            if (!string.Equals(videoDto.File.ContentType, "video/mp4", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid MIME type. Only video/mp4 is allowed");
            }
            var sasUrl = _configuration["BlobContainerSas"];
            if (string.IsNullOrEmpty(sasUrl))
                throw new Exception("BlobContainerSas is not configured");

            var containerClient = new BlobContainerClient(new Uri(sasUrl));
            var blobName = $"{videoDto.File.FileName}";
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = videoDto.File.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            var video = new TrainingVideo
            {
                Id = Guid.NewGuid(),
                Title = videoDto.Title,
                Description = videoDto.Description,
                UploadDate = DateTime.UtcNow,
                BlobUrl = blobClient.Uri.ToString()
            };

            await _trainingVideoRepository.Add(video);
            return video;
        }
    }
}

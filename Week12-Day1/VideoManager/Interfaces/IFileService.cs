using VideoManager.Models;

namespace VideoManager.Interfaces
{
    public interface IFileService
    {
        public Task<IEnumerable<TrainingVideo>> GetTrainingVideos();
        public Task<TrainingVideo> GetTrainingVideoById(Guid videoId);
        public Task<string> GetBlobUrl(Guid videoId);
        public Task<TrainingVideo> UploadTrainingVideo(UploadVideoDto videoDto);
        public Task DeleteTrainingVideo(Guid id);
    }
}
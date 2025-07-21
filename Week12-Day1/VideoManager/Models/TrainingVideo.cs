namespace VideoManager.Models
{
    public class TrainingVideo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string? BlobUrl { get; set; }
    }
}

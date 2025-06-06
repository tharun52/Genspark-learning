namespace docuShare.Models.DTOs
{
    public class DocumentViewResponseDto
    {
        public string Filename { get; set; } = string.Empty;
        public string FileFormat { get; set; } = string.Empty;
        public int AccessLevel { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
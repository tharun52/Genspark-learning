using System.ComponentModel.DataAnnotations;

namespace docuShare.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Filename { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty;
        public string FileFormat { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public byte[]? Content { get; set; }
        public int AccessLevel { get; set; } = 0;
    }
}
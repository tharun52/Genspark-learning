
using System.ComponentModel.DataAnnotations;

namespace FirstApi.Models
{
    public class Files
    {
        [Key]
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public byte[]? Content { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string FileFormat { get; set; } = string.Empty;
    }
}
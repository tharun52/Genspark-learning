using System.ComponentModel.DataAnnotations;

namespace docuShare.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int AccessLevel { get; set; } = 0;
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
    }
}
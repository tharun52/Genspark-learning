using System.ComponentModel.DataAnnotations;

namespace docuShare.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; } = string.Empty;
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
        public Role? Role { get; set; }
    }
}
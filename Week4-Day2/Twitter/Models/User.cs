using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public ICollection<Tweet>? Tweets { get; set; }
        public ICollection<User>? Followers { get; set; }
        public ICollection<User>? Following { get; set; }
    }
}
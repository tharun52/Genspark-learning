using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<TweetHastags>? Hastags { get; set; }

        public ICollection<Like>? Likes { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
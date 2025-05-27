using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class Hastag
    {
        [Key]
        public int HastagId { get; set; }
        public string Tag { get; set; } = string.Empty;
        public ICollection<TweetHastags>? TweetHastags{ get; set; }
    }
}
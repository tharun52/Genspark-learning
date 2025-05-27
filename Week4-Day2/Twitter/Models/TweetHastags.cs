using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class TweetHastags
    {
        [Key]
        public int TweetHastagsId { get; set; }
        public int TweetId { get; set; }
        public int HastagId { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet { get; set; }
        [ForeignKey("HastagId")]
        public Hastag? Hastag { get; set; }
    }
}
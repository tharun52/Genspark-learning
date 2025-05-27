using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TweetId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet{ get; set; }
    }
}
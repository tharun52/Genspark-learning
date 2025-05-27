using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class UserFollow
    {
        [Key]
        public int Id { get; set; }
        public int FollowingId { get; set; }
        public int FollowerId {get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("FollowerId")]
        public User? Follower { get; set; }

        [ForeignKey("FollowingId")]
        public User? Following{ get; set; }
    }
}
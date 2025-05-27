using Twitter.Models;
using Microsoft.EntityFrameworkCore;

namespace Twitter.Contexts
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Tweet> tweets { get; set; }
        public DbSet<TweetHastags> tweethastags { get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Hastag> hastags { get; set; }
        public DbSet<UserFollow> userfollows{ get; set; }
    }
}
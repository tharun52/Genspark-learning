using Microsoft.EntityFrameworkCore;
using VideoManager.Models;

namespace VideoManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrainingVideo> TrainingVideos { get; set; }
    }
}

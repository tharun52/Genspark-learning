using AzureApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Contexts
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
    }
}
using docuShare.Models;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Contexts
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }
        public DbSet<Models.Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Document>()
                .HasIndex(d => d.Filename)
                .IsUnique();
                modelBuilder.Entity<Role>()
                    .Property(r => r.AccessLevel)
                    .HasDefaultValue(1)
                    .IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
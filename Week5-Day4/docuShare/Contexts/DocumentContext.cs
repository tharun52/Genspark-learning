using docuShare.Models;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Contexts
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
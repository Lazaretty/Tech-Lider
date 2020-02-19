using Microsoft.EntityFrameworkCore;

namespace TechLider.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DBContext()
        {
        }
    }
}

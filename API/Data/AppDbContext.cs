using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Entities.AppUser> Users { get; set; } // DbSet untuk mengakses data user di database.
    }
}
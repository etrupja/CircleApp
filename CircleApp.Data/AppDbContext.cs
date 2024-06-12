using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}

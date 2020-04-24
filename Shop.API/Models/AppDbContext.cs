using Microsoft.EntityFrameworkCore;

namespace Shop.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        { }
        DbSet<Product> Products { get; set; }
    }
}

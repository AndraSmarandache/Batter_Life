using Microsoft.EntityFrameworkCore;

namespace BatterLife.Models
{
    public class BatterLifeDbContext : DbContext
    {
        public BatterLifeDbContext(DbContextOptions<BatterLifeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product> Products { get; set; } // Adaugă și celelalte modele dacă sunt necesare
    }
}
using Microsoft.EntityFrameworkCore;

namespace BuildingSardinia.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=building_sardinia;Username=lilla;Password=sardinia");
            }
        }
    }
}

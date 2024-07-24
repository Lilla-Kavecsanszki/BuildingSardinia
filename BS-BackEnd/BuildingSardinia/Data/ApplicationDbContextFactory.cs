using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BuildingSardinia.Models; // Ensure this matches the namespace of your ApplicationDbContext class

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite("Data Source=building_sardinia.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

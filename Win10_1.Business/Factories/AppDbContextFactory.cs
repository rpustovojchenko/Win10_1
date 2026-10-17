using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Win10_1.Business.Data;

namespace Win10_1.Business.Factories;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(Config.GetConnectionString());

        return new AppDbContext(optionsBuilder.Options);
    }
}

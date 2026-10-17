using Microsoft.EntityFrameworkCore;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data;

public class AppDbContext : DbContext
{
    private readonly string _connectionString = null!;

    public DbSet<User>              Users       { get; set; } = null!;
    public DbSet<Project>           Projects    { get; set; } = null!;
    public DbSet<ProjectTask>       Tasks       { get; set; } = null!;
    public DbSet<ProjectComment>    Comments    { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && _connectionString is not null)
            optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

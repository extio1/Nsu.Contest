namespace Nsu.Contest.Database;

using Microsoft.EntityFrameworkCore;
using Nsu.Contest.Entity;

using Microsoft.Extensions.Options;
using Nsu.Contest.Util;

public class ContestDbContext : DbContext
{
    public DbSet<Junior> Juniours { get; set; }
    public DbSet<Teamlead> Teamleads { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Contest> Contests { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }

    private readonly string _connectionString;

    public ContestDbContext(IOptions<ConfigurationDatabase> configuration)
    {
        _connectionString = configuration.Value.DatabaseConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContestDbContext).Assembly);
    }
}


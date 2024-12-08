namespace Nsu.Contest.Database;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Nsu.Contest.Entity;
using Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.Extensions.Options;
using Nsu.Contest.Util;

public class ContestDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Junior> Juniors { get; set; }
    public DbSet<Teamlead> Teamleads { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Contest> Contests { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }

    public ContestDbContext(DbContextOptions<ContestDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContestDbContext).Assembly);
    }
}


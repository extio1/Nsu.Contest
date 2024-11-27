namespace Nsu.Contest.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ContestDbContextFactory : IDesignTimeDbContextFactory<ContestDbContext>
{
    public ContestDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContestDbContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=contest;Username=extio1;Password=");

        return new ContestDbContext(optionsBuilder.Options);
    }
}

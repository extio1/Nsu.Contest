namespace Nsu.Contest.TestContainers;

using Xunit;
using Microsoft.EntityFrameworkCore;

using Nsu.Contest.Database;
using Nsu.Contest.Entity;
using Nsu.Contest.Contest;

public class DatabaseTest : IClassFixture<PostgresContaineredDatabaseFixture>
{
    private readonly PostgresContaineredDatabaseFixture _fixture;

    public DatabaseTest(PostgresContaineredDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CanInsertContestIntoDatabase()
    {
        var options = new DbContextOptionsBuilder<ContestDbContext>()
            .UseNpgsql(_fixture.Container.GetConnectionString())
            .Options;
        using var context = new ContestDbContext(options);
        await context.Database.EnsureCreatedAsync();

        EntityFactory factory = new(context); 
        Junior[] juniors = [factory.CreateJunior(1, "Ivan"), factory.CreateJunior(2, "Petr")];
        Teamlead[] teamleads = [factory.CreateTeamlead(3, "Sergey"), factory.CreateTeamlead(4, "Vladimir")];
        Wishlist[] juniorWishlists = [factory.CreateWishlist(juniors[0], [3, 4]), factory.CreateWishlist(juniors[1], [4, 3])];
        Wishlist[] teamleadWishlists = [factory.CreateWishlist(teamleads[0], [1, 2]), factory.CreateWishlist(teamleads[1], [2, 1])];
        Team[] teams = [factory.CreateTeam(teamleads[0], juniors[0]), factory.CreateTeam(teamleads[1], juniors[1])];

        Contest contest = factory.CreateContest(teamleads, juniors, teams, 42); 

        await context.SaveChangesAsync();

        var savedContest = context.Contests.FirstOrDefault(c => c.Id == contest.Id);

        Assert.NotNull(savedContest);
        Assert.Equal(contest.Score, savedContest.Score);
        Assert.Equal(contest.Id, savedContest.Id);
    }

    [Fact]
    public async Task CalculateAverageHarmonic()
    {
        var options = new DbContextOptionsBuilder<ContestDbContext>()
            .UseNpgsql(_fixture.Container.GetConnectionString())
            .Options;
        using var context = new ContestDbContext(options);
        await context.Database.EnsureCreatedAsync();
        EntityFactory factory = new(context); 

        Junior[] juniors = [factory.CreateJunior(10, "Ivan"), factory.CreateJunior(20, "Petr")];
        Teamlead[] teamleads = [factory.CreateTeamlead(30, "Sergey"), factory.CreateTeamlead(40, "Vladimir")];

        await context.SaveChangesAsync();

        Contest[] contests = [
            CreateContestWithHarmonic(context, factory, 10), 
            CreateContestWithHarmonic(context, factory, 20),
            CreateContestWithHarmonic(context, factory, 30)
        ];

        await context.SaveChangesAsync();
        
        Assert.Equal(20, ContestService.CalculateHarmonicMeanFor(contests));
    }

    private Contest CreateContestWithHarmonic(ContestDbContext context, EntityFactory factory, double score) {
        Junior[] juniors = context.Juniors.ToArray();
        Teamlead[] teamleads = context.Teamleads.ToArray();

        Team[] teams = [factory.CreateTeam(teamleads[0], juniors[0]), factory.CreateTeam(teamleads[1], juniors[1])];

        return factory.CreateContest(teamleads, juniors, teams, score);
    }
}

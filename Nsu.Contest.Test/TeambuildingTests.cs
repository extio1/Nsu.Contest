namespace Nsu.Contest.Test;

using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Teambuilding.Strategy;

using Moq;

// TODO: Возможно выделить свой класс для данных, потому что в theory методах всегда на входе
// требуются только juniors, teamleds, juniorsWishlists, teamleadsWishlists
public class TeambuildingTests : JuniorsTeamleadsTestData
{
    [Theory]
    [MemberData(nameof(JuniorsTeamleadsLists))]
    public void BuildTeams_WhenJuniourTeamleadsKnown_TeamsCountEqualToEmployeesCount(
        IEnumerable<Employee> juniors, IEnumerable<Employee> teamleads
    )
    {
        var manager = new Manager(new RandomTeamBuildingStrategy());
        var wishlistGenerator = new WishlistGenerator();
        var juniorsWishlists = wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var teamleadsWishlists = wishlistGenerator.GenerateWishlists(teamleads, juniors);
    
        var teams = manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        Assert.Equal(teams.Count(), juniors.Count());
        Assert.Equal(teams.Count(), teamleads.Count());
    }

    [Theory]
    [MemberData(nameof(JuniorsTeamleadsLists))]
    public void BuildTeams_WhenJuniourTeamleadsKnown_DistributionRight(
        IEnumerable<Employee> juniors, IEnumerable<Employee> teamleads
    )
    {
        var manager = new Manager(new EqualIdsBuildingStrategy());
        var wishlistGenerator = new WishlistGenerator();
        var juniorsWishlists = wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var teamleadsWishlists = wishlistGenerator.GenerateWishlists(teamleads, juniors);
        
        var teams = manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);
        
        foreach (var team in teams)
        {
            Assert.Equal(team.Teamlead.Id, team.Junior.Id);
        }
    }

    [Theory]
    [MemberData(nameof(JuniorsTeamleadsLists))]
    public void BuildTeams_WhenBuildingTeams_CallsStrategyOnlyOnce(
        IEnumerable<Employee> juniors, IEnumerable<Employee> teamleads
    )
    {
        var mockService = new Mock<ITeamBuildingStrategy>();
        var manager = new Manager(mockService.Object);
        var wishlistGenerator = new WishlistGenerator();
        var juniorsWishlists = wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var teamleadsWishlists = wishlistGenerator.GenerateWishlists(teamleads, juniors);

        
        // mock.setup(service => service.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists))
        
        manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        mockService.Verify
        (
            s => s.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists), 
            Times.Once()
        );
    }

    [Fact]
    public void BuildTeams_WhenBuildingTeamsOfDifferentSizes_ThrowsException()
    {
        var juniors = new List<Employee>(){new(1, "John"), new(2, "John")};
        var teamleads = new List<Employee>(){new(1, "Alexa")};
        var juniorsWishlists = new List<Wishlist>(){new(1, new int[]{1}), new(2, new int[]{1})};
        var teamleadsWishlists = new List<Wishlist>(){new(1, new int[]{2})};
        var mockService = new Mock<ITeamBuildingStrategy>();
        var manager = new Manager(mockService.Object);
        mockService.Setup(s => s.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists))
                .Throws(new ArgumentException("All collections must be the same length."));
        
        
        var action = () => manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Equal("All collections must be the same length.", exception.Message);
    }
}
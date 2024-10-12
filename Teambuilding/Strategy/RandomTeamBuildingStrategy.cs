namespace Nsu.Contest.Teambuilding.Strategy;

using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;

public sealed class RandomTeamBuildingStrategy : ITeamBuildingStrategy
{
    private readonly RandomGenerator _randomGenerator;

    public RandomTeamBuildingStrategy(RandomGenerator randomGenerator)
    {
        _randomGenerator = randomGenerator;
    }

    /// <summary>
    /// Random algorithm of teambuilding.
    /// </summary>
    /// <param name="teamLeads"></param>
    /// <param name="juniors"></param>
    /// <param name="teamLeadsWishlists"></param>
    /// <param name="juniorsWishlists"></param>
    /// <returns> Random distribution of team members </returns>
    public IEnumerable<Team> BuildTeams(
            IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists
        )
    {
        var teams = new List<Team>();

        var randPermTeamleads = _randomGenerator.GenerateRandomPermutation(20);
        var randPermJuniors = _randomGenerator.GenerateRandomPermutation(20);

        var teamleadsList = new List<Employee>(teamLeads);
        var juniorsList = new List<Employee>(juniors);

        for (var i = 0; i < 20; i++)
        {
            teams.Add(new Team(teamleadsList[randPermTeamleads[i] - 1], juniorsList[randPermJuniors[i] - 1]));
        }

        return teams;
    }
}

namespace Nsu.Contest.Teambuilding.Strategy;

using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;
using Microsoft.Extensions.Options;

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
    /// <param name="teamleads"></param>
    /// <param name="juniors"></param>
    /// <param name="teamleadsWishlists"></param>
    /// <param name="juniorsWishlists"></param>
    /// <returns> Random distribution of team members </returns>
    public IEnumerable<Team> BuildTeams(
            IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists
        )
    {
        if((teamleads.Count() != juniors.Count())  || 
           (teamleadsWishlists.Count() != juniorsWishlists.Count()) || 
           (teamleads.Count() != teamleadsWishlists.Count()))
        {
            throw new ArgumentException("All collections must be the same length.");
        }

        var teams = new List<Team>();

        var randPermteamleads = _randomGenerator.GenerateRandomPermutation(juniors.Count());
        var randPermJuniors = _randomGenerator.GenerateRandomPermutation(juniors.Count());

        var teamleadsList = new List<Employee>(teamleads);
        var juniorsList = new List<Employee>(juniors);

        for (var i = 0; i < 20; i++)
        {
            teams.Add(new Team(teamleadsList[randPermteamleads[i] - 1], juniorsList[randPermJuniors[i] - 1]));
        }

        return teams;
    }
}

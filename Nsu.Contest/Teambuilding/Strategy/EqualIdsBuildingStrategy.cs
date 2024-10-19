namespace Nsu.Contest.Teambuilding.Strategy;

using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;

public sealed class EqualIdsBuildingStrategy : ITeamBuildingStrategy
{
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

        var employeeCount = teamleads.Count();

        var teams = new List<Team>();

        var teamleadsList = new List<Employee>(teamleads);
        var juniorsList = new List<Employee>(juniors);

        foreach (var teamlead in teamleadsList)
        {
            var junior = juniorsList.Find(j => teamlead.Id == j.Id);
            teams.Add(new Team(teamlead, junior));
        }

        return teams;
    }
}


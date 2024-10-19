namespace Nsu.Contest.Teambuilding;

using Nsu.Contest.Entity;

public class Manager
{
    private readonly ITeamBuildingStrategy _teamBuildingStrategy;

    public Manager(ITeamBuildingStrategy teamBuildingStrategy)
    {
        _teamBuildingStrategy = teamBuildingStrategy;
    }

    public IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return _teamBuildingStrategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
    }
}

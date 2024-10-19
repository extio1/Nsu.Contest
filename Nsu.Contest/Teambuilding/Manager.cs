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
        IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return _teamBuildingStrategy.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);
    }
}

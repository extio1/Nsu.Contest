namespace Nsu.Contest.Contest;

using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;

public class Contest
{
    readonly private Director _director;
    readonly private Manager _manager;
    readonly private IWishlistGenerator _wishlistGenerator;

    public Contest(Director director, Manager manager, IWishlistGenerator wishlistGenerator)
    {
        _director = director;
        _manager = manager;
        _wishlistGenerator = wishlistGenerator;
    }
    public double Run(IEnumerable<Teamlead> teamleads, IEnumerable<Junior> juniors)
    {
        var juniorsWishlists = _wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var teamleadsWishlists = _wishlistGenerator.GenerateWishlists(juniors, teamleads);

        var teams = _manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        return _director.EstimateTeams(juniorsWishlists, teamleadsWishlists, teams);
    }
}

namespace Nsu.Contest.Contest;

using Microsoft.Extensions.Options;
using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;

public class Contest
{
    readonly private Director _director;
    readonly private Manager _manager;
    readonly private WishlistGenerator _wishlistGenerator;
    // Можно сделать WishlistGenerator утилитный классом, чтобы не передавать ссылку на него сюда,
    // он все равно синглтон, состояния никакого нет?
    public Contest(Director director, Manager manager, WishlistGenerator wishlistGenerator)
    {
        _director = director;
        _manager = manager;
        _wishlistGenerator = wishlistGenerator;
    }
    public double Run(IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors)
    {
        var juniorsWishlists = _wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var teamleadsWishlists = _wishlistGenerator.GenerateWishlists(juniors, teamleads);

        var teams = _manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        return _director.EstimateTeams(juniorsWishlists, teamleadsWishlists, teams);
    }
}

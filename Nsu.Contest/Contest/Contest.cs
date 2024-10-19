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
    readonly private RandomGenerator _randomGenerator;
    // Можно сделать RandomGenerator утилитный классом, чтобы не передавать ссылку на него сюда,
    // он все равно синглтон, состояния никакого нет?
    public Contest(Director director, Manager manager, RandomGenerator randomGenerator)
    {
        _director = director;
        _manager = manager;
        _randomGenerator = randomGenerator;
    }
    public double Run(IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors)
    {
        var juniorsWishlists = _randomGenerator.GenerateWishlists(juniors.Count());
        var teamleadsWishlists = _randomGenerator.GenerateWishlists(teamleads.Count());

        var teams = _manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        return _director.EstimateTeams(juniorsWishlists, teamleadsWishlists, teams);
    }
}

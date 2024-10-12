namespace Nsu.Contest.Contest;

using Nsu.Contest.Util;
using Nsu.Contest.Entity;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;

public class Contest
{
    readonly private Director _director;
    readonly private Manager _manager;
    readonly private IOptions<Configuration> _configuration;
    readonly private RandomGenerator _randomGenerator;
    public Contest(Director director, Manager manager, IOptions<Configuration> configuration, RandomGenerator randomGenerator)
    {
        Console.WriteLine("CONTEST OBJECT CREATED");
        _director = director;
        _manager = manager;
        _configuration = configuration;
        _randomGenerator = randomGenerator;
    }
    public double Run(IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors)
    {
        var juniorsWishlists = _randomGenerator.GenerateWishlists(_configuration.Value.nRounds);
        var teamleadsWishlists = _randomGenerator.GenerateWishlists(_configuration.Value.nRounds);

        var teams = _manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        return _director.EstimateTeams(_configuration.Value.nRounds, juniorsWishlists, teamleadsWishlists, teams);
    }
}

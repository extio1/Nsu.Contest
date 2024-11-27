using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Nsu.Contest.Database;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;

namespace Nsu.Contest.Contest;

public class ContestService
{
    private readonly EntityFactory _factory;
    private readonly ContestDbContext _contestDbContext;
    readonly private Director.Director _director;
    readonly private Manager _manager;
    readonly private WishlistGenerator _wishlistGenerator;


    public ContestService(EntityFactory factory, ContestDbContext contestDbContext,
                        Director.Director director, Manager manager, WishlistGenerator wishlistGenerator)
    {
        _factory = factory;
        _contestDbContext = contestDbContext;
        _director = director;
        _manager = manager;
        _wishlistGenerator = wishlistGenerator;
    }
// using var transaction = dbContext.Database.BeginTransaction();
    public void MakeContest()
    {
        var juniors = _contestDbContext.Juniors.ToList();
        var teamleads = _contestDbContext.Teamleads.ToList();

        var juniorsWishlists = _wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var teamleadsWishlists = _wishlistGenerator.GenerateWishlists(juniors, teamleads);

        var teams = _manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        Console.WriteLine("Teams");

        var score = _director.EstimateTeams(juniorsWishlists, teamleadsWishlists, teams);

        Console.WriteLine("Estimated", score);

        _factory.CreateContest(teamleads, juniors, teams, score);

        Console.WriteLine("Contest", score);

        int entries_count = _contestDbContext.SaveChanges();
        Console.WriteLine(entries_count);
    }

    public void LogToConsoleContestInfo(Guid id)
    {
        var contestsQuery = _contestDbContext.Contests.Where(c => c.Id == id)
            .Include(c => c.Teams)
            .ThenInclude(t => t.Teamlead)
            .Include(c => c.Teams)                
            .ThenInclude(t => t.Junior);
    
        foreach (var contest in contestsQuery.ToList())
        {
            Console.WriteLine($"Id = {contest.Id}");
            Console.WriteLine($"Score = {contest.Score}");
            Console.WriteLine($"Teams:");
            var teams = contestsQuery.SelectMany(c => c.Teams);  
            foreach (var team in teams)
            {
                Console.WriteLine($"Teamlead: {team.Teamlead.Id} - {team.Teamlead.Name} and junior: {team.Junior.Id} - {team.Junior.Name}");
            }
        }
    }

    public double CalculateHarmonicMeanForAll()
    {
        var contests = _contestDbContext.Contests;
        
        var sum = 0.0;
        foreach (var contest in contests)
        {
            sum += contest.Score;
        }

        return sum / contests.Count();
    }
}

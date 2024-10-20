namespace Nsu.Contest.Director;

using Nsu.Contest.Entity;

public class Director
{
    ITeamEstimatingStrategy _estimatingStrategy;
    public Director(ITeamEstimatingStrategy estimatingStrategy) 
    {
        _estimatingStrategy = estimatingStrategy;
    }

    /// <summary>
    /// Calculate mean harmonic of teams distribution
    /// </summary>
    /// <param name="nPartisipants"></param>
    /// <param name="juniorsWishlists"></param>
    /// <param name="teamleadsWishlists"></param>
    /// <param name="teams"></param>
    /// <returns>Mean harmonic of teams distribution</returns>
    public double EstimateTeams(IEnumerable<Wishlist> juniorsWishlists, IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Team> teams)
    {
        if((juniorsWishlists.Count() != teamleadsWishlists.Count())  || 
           (teamleadsWishlists.Count() != teams.Count()))
        {
            throw new ArgumentException("All three collections must be the same length.");
        }

        var employeePoints = teams.Select(t => t.Junior.GetSatisfactionPoint(juniorsWishlists, t.Teamlead))
        .Concat(teams.Select(t => t.Teamlead.GetSatisfactionPoint(teamleadsWishlists, t.Junior))).ToArray();

        return _estimatingStrategy.Calculate(employeePoints);
    }
}


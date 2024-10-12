namespace Nsu.Contest.Director;

using Nsu.Contest.Entity;

public class Director
{
    private double _harmonicMeanAvg { set; get; }
    private double _n { set; get; }
    public Director() 
    { 
        _harmonicMeanAvg = 0; 
        _n = 0; 
    }

    /// <summary>
    /// Calculate mean harmonic of teams distribution
    /// </summary>
    /// <param name="nPartisipants"></param>
    /// <param name="juniorsWishlists"></param>
    /// <param name="teamleadsWishlists"></param>
    /// <param name="teams"></param>
    /// <returns>Mean harmonic of teams distribution</returns>
    public double EstimateTeams(int nPartisipants, List<Wishlist> juniorsWishlists, List<Wishlist> teamleadsWishlists, IEnumerable<Team> teams)
    {
        var sumReciprocals = 0.0;

        foreach (var team in teams)
        {
            sumReciprocals += 1.0 / team.Junior.GetSatisfactionPoints(juniorsWishlists, team.TeamLead);
            sumReciprocals += 1.0 / team.TeamLead.GetSatisfactionPoints(teamleadsWishlists, team.Junior);
        }

        return nPartisipants / sumReciprocals;
    }
}


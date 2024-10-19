namespace Nsu.Contest.Director;

using Nsu.Contest.Entity;

public class Director
{
    public Director() {}

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

        var sumReciprocals = 0.0;

        foreach (var team in teams)
        {
            sumReciprocals += 1.0 / team.Junior.GetSatisfactionPoints(juniorsWishlists, team.Teamlead);
            sumReciprocals += 1.0 / team.Teamlead.GetSatisfactionPoints(teamleadsWishlists, team.Junior);
        }

        return  teams.Count() / sumReciprocals;
    }
}


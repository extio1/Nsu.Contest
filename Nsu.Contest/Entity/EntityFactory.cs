using Nsu.Contest.Database;

namespace Nsu.Contest.Entity;

public class EntityFactory
{
    private readonly ContestDbContext _contestDbContext;
    public EntityFactory(ContestDbContext contestDbContext)
    {
        _contestDbContext = contestDbContext;
    }
    public Junior CreateJunior(int id, string name)
    {
        var junior = new Junior(id, name);
        _contestDbContext.Juniors.Add(junior);
        return junior;
    }
    public Teamlead CreateTeamlead(int id, string name)
    {
        var teamlead = new Teamlead(id, name);
        _contestDbContext.Teamleads.Add(teamlead);
        return teamlead;
    }
    public Team CreateTeam(Teamlead teamlead, Junior junior)
    {
        var team = new Team(teamlead, junior);
        _contestDbContext.Teams.Add(team);
        return team;
    }
    public Wishlist CreateWishlist(Employee emplFor, int[] emplList)
    {
        var wishlist = new Wishlist(emplFor, emplList); 
        _contestDbContext.Wishlists.Add(wishlist);
        return wishlist;
    }
    public Contest CreateContest(IEnumerable<Teamlead> teamleads, IEnumerable<Junior> juniours, 
                                 IEnumerable<Team> teams, double score)
    {
        var contest = new Contest(teamleads, juniours, teams, score);
        _contestDbContext.Contests.Add(contest);
        return contest;
    }
}

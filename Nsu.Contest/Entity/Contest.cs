namespace Nsu.Contest.Entity;

public class Contest {
    public Contest() 
    {
        Teamleads = new List<Teamlead>();
        Juniors = new List<Junior>();
        Teams = new List<Team>();
    }
    
    internal Contest(IEnumerable<Teamlead> teamleads, IEnumerable<Junior> juniours, 
                    IEnumerable<Team> teams, double score)
    {
        Teamleads = teamleads;
        Juniors = juniours;
        Teams = teams;
        Score = score;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Score { get; set; }
    public IEnumerable<Teamlead> Teamleads { get; set; }
    public IEnumerable<Junior> Juniors { get; set; }
    public IEnumerable<Team> Teams { get; set; }
};

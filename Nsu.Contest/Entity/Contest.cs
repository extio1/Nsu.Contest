namespace Nsu.Contest.Entity;

public class Contest(List<Teamlead> teamleads, List<Junior> juniours, List<Team> teams) {  
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<Teamlead> Teamleads { get; set; } = teamleads;
    public List<Junior> Juniors { get; set; } = juniours;
    public List<Team> Teams { get; set; } = teams;
    public double Points { get; set; } = 0;
};

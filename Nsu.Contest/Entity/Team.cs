namespace Nsu.Contest.Entity;

public class Team(Teamlead teamlead, Junior junior)
{  
    public Guid Id { get; set; } = Guid.NewGuid();
    public Teamlead Teamlead { get; set; } = teamlead;
    public Junior Junior { get; set; } = junior;
};

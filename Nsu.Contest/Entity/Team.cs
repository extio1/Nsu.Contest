namespace Nsu.Contest.Entity;

public class Team
{  
    public Team() { }
    internal Team (Teamlead teamlead, Junior junior)
    {
        Teamlead = teamlead;
        Junior = junior;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Teamlead Teamlead { get; set; }
    public Junior Junior { get; set; }
}

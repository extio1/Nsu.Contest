namespace Nsu.Contest.Director;

public interface ITeamEstimatingStrategy 
{
    public double Calculate(IEnumerable<double> inputSequence);
}

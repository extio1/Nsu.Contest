namespace Nsu.Contest.Director;

public interface ITeamEstimatingStrategy 
{
    public double Calculate(double[] inputSequence);
}

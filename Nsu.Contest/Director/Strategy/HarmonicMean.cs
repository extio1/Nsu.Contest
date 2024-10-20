using Nsu.Contest.Director;

namespace Nsu.Contest.Director;

public class HarmonicMean : ITeamEstimatingStrategy
{
    public double Calculate(double[] inputSequence) {
        double sumReciprocals = 0.0;

        foreach (var num in inputSequence)
        {
            if(num <= 0) {
                throw new ArgumentException($"All elements of inputSequence must be positive, but {num} found");
            }
            sumReciprocals += 1 / num;
        }

        return inputSequence.Length / sumReciprocals;
    }
}

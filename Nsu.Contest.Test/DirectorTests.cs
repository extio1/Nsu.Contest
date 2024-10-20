using Nsu.Contest.Director;

namespace Nsu.Contest.Test;

using Nsu.Contest.Entity;
using Nsu.Contest.Director;

public class DirectorTests : WishlistsTeamsHarmonicTestData
{
    [Theory]
    [InlineData(new double[]{1, 1, 1})]
    [InlineData(new double[]{42.42, 42.42, 42.42, 42.42, 42.42, 42.42})]
    [InlineData(new double[]{42424242424242424, 42424242424242424, 42424242424242424, 42424242424242424, 42424242424242424})]
    public void HarmonicMean_WhenInputIsEqualNumbers_ResulEqualToSumOfNumbers(double[] inputSequence){
        var harmonicMean = new HarmonicMean();

        Assert.Equal(inputSequence[0], harmonicMean.Calculate(inputSequence));
    }

    [Theory]
    [InlineData(new double[]{2, 6}, 3)]
    [InlineData(new double[]{1, 2, 3, 2, 1}, 1.5)]
    public void HarmonicMean_CalculatesRigth(double[] inputSequence, double result){
        var harmonicMean = new HarmonicMean();

        Assert.Equal(result, harmonicMean.Calculate(inputSequence), precision: 3);
    }

    [Theory]
    [InlineData(new double[]{1, 2, 3, 0, 0})]
    [InlineData(new double[]{0, 2, 3, 10, 10})]
    [InlineData(new double[]{-52, 52, 52, 52})]
    [InlineData(new double[]{1, 2, 3, -1, 100})]
    public void HarmonicMean_WhenInputHasNonPositiveValues_ThrowsArgumentException(double[] inputSequence){
        var harmonicMean = new HarmonicMean();

        var exception = Assert.Throws<ArgumentException>(() => harmonicMean.Calculate(inputSequence));
        // TODO проверка сообщения исключения, формируемого на основе входных данных
        // Assert.Equal("All elements of inputSequence must be positive, but {num} found", exception.Message);
    }

    [Theory]
    [MemberData(nameof(WishlistsTeamsHarmonic))]
    public void Director_EstimateTeams_Correctly(
        IEnumerable<Wishlist> juniorsWishlists, IEnumerable<Wishlist> teamleadsWishlists,
        IEnumerable<Team> teams, double harmonicExpected 
    )
    {
        var director = new Director(new HarmonicMean());
        Assert.Equal(harmonicExpected, 
                     director.EstimateTeams(juniorsWishlists, teamleadsWishlists, teams), precision:3);
    }
}

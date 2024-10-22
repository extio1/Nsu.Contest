namespace Nsu.Contest.Test;

using Moq;

using Nsu.Contest.Entity;
using Nsu.Contest.Contest;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Teambuilding.Strategy;
using Nsu.Contest.Util;

public class ContestTest : EmployeesHarmonicTestData
{
    [Theory]
    [MemberData(nameof(EmployeesHarmonic))]
    public void Contest_WhenEmployeesKnown_HarmonicMeanEqualToExpected(
        IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors, 
        IEnumerable<Wishlist> teamleadsWishlist, IEnumerable<Wishlist> juniorsWishlist,
         double expectedHarmonic
    ){
        var manager = new Manager(new EqualIdsBuildingStrategy());
        var director = new Director(new HarmonicMean());
        var wishlistGeneratorMock = new Mock<IWishlistGenerator>();
        wishlistGeneratorMock.Setup(m => m.GenerateWishlists(teamleads, juniors)).Returns(teamleadsWishlist);
        wishlistGeneratorMock.Setup(m => m.GenerateWishlists(juniors, teamleads)).Returns(juniorsWishlist);
        var contest = new Contest(director, manager, wishlistGeneratorMock.Object);

        var actualHarmonic = contest.Run(teamleads, juniors);

        Assert.Equal(expectedHarmonic, actualHarmonic, precision:3);
    }
}

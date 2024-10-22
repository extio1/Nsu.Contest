namespace Nsu.Contest.Test;

using Nsu.Contest.Entity;
using Nsu.Contest.Util;

public class WishlistGenerationTests : JuniorsTeamleadsTestData
{
    [Theory]
    [MemberData(nameof(JuniorsTeamleadsLists))]
    public void Generate_WhenTeamleadsJuniorsListsCountKnown_WishlistHasTheSameCount(
        IEnumerable<Employee> juniors, IEnumerable<Employee> teamleads
    )
    {
        var wishlistGenerator = new WishlistGenerator();
        
        var wishlists = wishlistGenerator.GenerateWishlists(juniors, teamleads);

        Assert.Equal(wishlists.Count(), juniors.Count());
        foreach (var wishlist in wishlists)
        {
            Assert.Equal(wishlist.DesiredEmployees.Length, teamleads.Count());
        }
    }

    [Theory]
    [MemberData(nameof(JuniorsTeamleadsLists))]
    public void Generate_WhenSomeEmployeeKnown_SomeEmployeeExistInWishlist(
        IEnumerable<Employee> juniors, IEnumerable<Employee> teamleads
    )
    { 
        var wishlistGenerator = new WishlistGenerator();
        
        var wishlistsForJuniors = wishlistGenerator.GenerateWishlists(juniors, teamleads);
        var wishlistsForTeamleads = wishlistGenerator.GenerateWishlists(teamleads, juniors);

        foreach (var junior in juniors)
        {
            Assert.Contains(junior.Id, wishlistsForJuniors.Select(w => w.EmployeeId));
        }

        foreach (var teamlead in teamleads)
        {
            Assert.Contains(teamlead.Id, wishlistsForTeamleads.Select(w => w.EmployeeId));
        }
    }
}
namespace Nsu.Contest.Test;

using Nsu.Contest.Director;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Teambuilding.Strategy;
using Nsu.Contest.Util;

public abstract class TestData 
{
    private const int UserTestDataNamesLength = 10;
    
    public static List<Employee> GenerateEmployeeList(int length)
    {
        var list = new List<Employee>(length);
        for (var i = 1; i <= length; i++) {
            list.Add(new Employee(i, RandomGenerator.GenerateRandomString(UserTestDataNamesLength)));
        }
        return list;
    }
    public static (List<Employee>, List<Employee>) GenerateJuniorsTeamleadsLists(int n)
    {
        return (GenerateEmployeeList(n), GenerateEmployeeList(n));
    }
    public static (IEnumerable<Wishlist>, IEnumerable<Wishlist>) GenerateWishlists(IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors)
    {
        WishlistGenerator wishlistGenerator = new();
        return (
            wishlistGenerator.GenerateWishlists(teamleads, juniors),
            wishlistGenerator.GenerateWishlists(juniors, teamleads)
        );
    }
    public static IEnumerable<Team> GenerateTeams(
        IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists
        )
    {
        Manager manager = new(new EqualIdsBuildingStrategy());
        return manager.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);
    }

    public static (IEnumerable<Employee>, IEnumerable<Employee>, 
            IEnumerable<Wishlist>, IEnumerable<Wishlist>, 
            IEnumerable<Team>, double) 
    GenerateAllForContest(int n)
    {
        var harmonicMean = new HarmonicMean();
        var (teamleads, juniors) = GenerateJuniorsTeamleadsLists(n);
        var (teamleadsWishlists, juniorsWishlists) = GenerateWishlists(teamleads, juniors);
        var teams = GenerateTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);

        var points = teams.
            Select(t => t.Junior.GetSatisfactionPoint(juniorsWishlists, t.Teamlead)).
            Concat(teams.Select(t => t.Teamlead.GetSatisfactionPoint(teamleadsWishlists, t.Junior))).
            ToArray();

        return (teamleads, juniors, teamleadsWishlists, juniorsWishlists, teams, harmonicMean.Calculate(points));
    }
}

public class JuniorsTeamleadsTestData : TestData
{
    public static IEnumerable<object[]> JuniorsTeamleadsLists()
    {
        yield return GenerateEmployeeLists(10);
        yield return GenerateEmployeeLists(20);
        yield return GenerateEmployeeLists(5);
        yield return GenerateEmployeeLists(42);
        yield return GenerateEmployeeLists(1000);
        yield return GenerateEmployeeLists(0);
    }
    private static object[] GenerateEmployeeLists(int n) {
        var (teamleads, juniors) = GenerateJuniorsTeamleadsLists(n);
        return new object[]{teamleads, juniors};
    }
}

public class JuniorsTeamleadsWishlistsTestData : TestData
{
    public static IEnumerable<object[]> JuniorsTeamleadsWishlists()
    {
        yield return GenerateJuniorsTeamleadsAndWishlists(10);
        yield return GenerateJuniorsTeamleadsAndWishlists(20);
        yield return GenerateJuniorsTeamleadsAndWishlists(5);
        yield return GenerateJuniorsTeamleadsAndWishlists(42);
        yield return GenerateJuniorsTeamleadsAndWishlists(1000);
        yield return GenerateJuniorsTeamleadsAndWishlists(0);
    }
    private static object[] GenerateJuniorsTeamleadsAndWishlists(int n){
        var (teamleads, juniors) = GenerateJuniorsTeamleadsLists(n);
        var (teamleadsWishlists, juniorsWishlists) = GenerateWishlists(teamleads, juniors);

        return new object[]{teamleads, juniors, teamleadsWishlists, juniorsWishlists};
    }
}

public class WishlistsTeamsHarmonicTestData : TestData
{
    public static IEnumerable<object[]> WishlistsTeamsHarmonic()
    {
        yield return GenerateWishlistsTeamsHarmonic(10);
        yield return GenerateWishlistsTeamsHarmonic(20);
        yield return GenerateWishlistsTeamsHarmonic(5);
        yield return GenerateWishlistsTeamsHarmonic(42);
        yield return GenerateWishlistsTeamsHarmonic(1000);
        yield return GenerateWishlistsTeamsHarmonic(0);
    }
    private static object[] GenerateWishlistsTeamsHarmonic(int n){
        var (_, _, teamleadsWishlists, juniorsWishlists, teams, harmonic) = GenerateAllForContest(n);
        
        return new object[]{teamleadsWishlists, juniorsWishlists, teams, harmonic};
    }
}

// Использование случайно генерируемых данных в общем случае приводит к накоплению 
// большой разнице ошибок между подсчетами среднего гармонического как фактического
// и ожидаемого

public class EmployeesHarmonicTestData : TestData
{
    public static IEnumerable<object[]> EmployeesHarmonic()
    {
        yield return new object[]{
            new List<Employee>{new(1, "John"), new(2, "Sonya")},
            new List<Employee>{new(1, "Alexa"), new(2, "Greg")},
            new List<Wishlist>{new(1, new int[]{1, 2}), new(2, new int[]{1, 2})},
            new List<Wishlist>{new(1, new int[]{2, 1}), new(2, new int[]{2, 1})},
            1.333
            };
    }
}

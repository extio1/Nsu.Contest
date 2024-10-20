namespace Nsu.Contest.Test;

using Nsu.Contest.Entity;
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
}

public class JuniorsTeamleadsTestData : TestData
{
    public static IEnumerable<object[]> JuniorsTeamleadsLists()
    {
        yield return GenerateData(10);
        yield return GenerateData(20);
        yield return GenerateData(5);
        yield return GenerateData(42);
        yield return GenerateData(1000);
        yield return GenerateData(0);
    }
    private static object[] GenerateData(int n){
        return new object[]{GenerateEmployeeList(n), GenerateEmployeeList(n)};
    }
}

public class JuniorsTeamleadsAndWishlistsTestData : TestData
{
    public static IEnumerable<object[]> JuniorsTeamleadsAndWishlists()
    {
        yield return GenerateData(10);
        yield return GenerateData(20);
        yield return GenerateData(5);
        yield return GenerateData(42);
        yield return GenerateData(1000);
        yield return GenerateData(0);
    }
    private static object[] GenerateData(int n){
        WishlistGenerator wishlistGenerator = new WishlistGenerator();
        var juniours = GenerateEmployeeList(n);
        var teamleads = GenerateEmployeeList(n);
        var junioursWishlists = wishlistGenerator.GenerateWishlists(juniours, teamleads);
        var teamleadsWishlists = wishlistGenerator.GenerateWishlists(teamleads, juniours);

        return new object[]{juniours, teamleads, junioursWishlists, teamleadsWishlists};
    }
}


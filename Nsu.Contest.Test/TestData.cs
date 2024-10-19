namespace Nsu.Contest.Test;

using Nsu.Contest.Entity;
using Nsu.Contest.Util;

public abstract class TestData 
{
    private const int UserTestDataNamesLength = 10;
    
    public static List<Employee> generateEmployeeList(int length)
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
        yield return new object[] {
            generateEmployeeList(10),
            generateEmployeeList(10)
        };
        yield return new object[] {
            generateEmployeeList(20),
            generateEmployeeList(20)
        };
        yield return new object[] {
            generateEmployeeList(5),
            generateEmployeeList(5)
        };
        yield return new object[] {
            generateEmployeeList(42),
            generateEmployeeList(42)
        };
        yield return new object[] {
            generateEmployeeList(100),
            generateEmployeeList(100)
        };
        yield return new object[] {
            generateEmployeeList(0),
            generateEmployeeList(0)
        };
    }
}


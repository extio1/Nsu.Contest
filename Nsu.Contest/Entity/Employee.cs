namespace Nsu.Contest.Entity;

public class Employee
{
    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public double GetSatisfactionPoint(IEnumerable<Wishlist> emplsWishlists, Employee teammate)
    {
        var emplWishlist = emplsWishlists.First(e => e.ForEmployeeId == Id);
        var index = Array.IndexOf(emplWishlist.DesiredEmployees.ToArray(), teammate.Id);
        return emplsWishlists.Count() - index;
    }
}


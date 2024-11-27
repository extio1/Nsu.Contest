namespace Nsu.Contest.Entity;

public class Employee
{
    internal Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public double GetSatisfactionPoint(IEnumerable<Wishlist> emplsWishlists, Employee teammate)
    {

        var emplWishlist = emplsWishlists.First(e => e.ForEmployee.Id == Id);
        return emplsWishlists.Count() - Array.IndexOf(emplWishlist.DesiredEmployees.ToArray(), teammate.Id);
    }
}


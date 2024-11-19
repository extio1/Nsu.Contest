namespace Nsu.Contest.Entity;

public class Employee(int id, string name) {
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;

    public double GetSatisfactionPoint(IEnumerable<Wishlist> emplsWishlists, Employee teammate)
    {
        var emplWishlist = emplsWishlists.First(e => e.ForEmployee.Id == Id);
        return emplsWishlists.Count() - Array.IndexOf(emplWishlist.DesiredEmployees, teammate.Id);
    }
}

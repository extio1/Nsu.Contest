namespace Nsu.Contest.Entity;

public record Employee(int Id, string Name) {
    public double GetSatisfactionPoint(IEnumerable<Wishlist> emplsWishlists, Employee teammate)
    {
        var emplWishlist = emplsWishlists.First(e => e.EmployeeId == Id);
        return emplsWishlists.Count() - Array.IndexOf(emplWishlist.DesiredEmployees, teammate.Id);
    }
}

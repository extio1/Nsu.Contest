namespace Nsu.Contest.Entity;

public record Employee(int Id, string Name) {
    public double GetSatisfactionPoints(List<Wishlist> emplsWishlists, Employee teammate)
    {
        var emplWishlist = emplsWishlists.Find(e => e.EmployeeId == Id);
        return emplsWishlists.Count - Array.IndexOf(emplWishlist.DesiredEmployees, teammate.Id);
    }
}

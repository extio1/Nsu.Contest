namespace Nsu.Contest.Util;

using Nsu.Contest.Entity;

public class WishlistGenerator
{
    // TODO: probably refactor, because now GenerateWishlists break encapsulation of Employee
    // (knows that they uses integers as Id stating with 1) 
    public List<Wishlist> GenerateWishlists(IEnumerable<Employee> forEmpls, IEnumerable<Employee> ofEmpls)
    {
        if((forEmpls.Count() != ofEmpls.Count()))
        {
            throw new ArgumentException("All collections must be the same length.");
        }
        
        var employeesCount = forEmpls.Count();
        var wishlists = new List<Wishlist>(employeesCount);

        for (var i = 1; i <= employeesCount; ++i)
        {
            var prioritiesForEmpl = RandomGenerator.GeneratePermutation(employeesCount);
            wishlists.Add(new Wishlist(i, prioritiesForEmpl));
        }

        return wishlists;
    }
}

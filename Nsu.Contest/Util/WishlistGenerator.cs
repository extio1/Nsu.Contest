namespace Nsu.Contest.Util;

using Nsu.Contest.Entity;

public class WishlistGenerator : IWishlistGenerator
{

    public IEnumerable<Wishlist> GenerateWishlists(IEnumerable<Employee> forEmpls, IEnumerable<Employee> ofEmpls)
    {
        if(forEmpls.Count() != ofEmpls.Count())
        {
            throw new ArgumentException("All collections must be the same length.");
        }
        
        var employeesCount = forEmpls.Count();
        var wishlists = new List<Wishlist>(employeesCount);

        foreach (var forEmpl in forEmpls)
        {
            var prioritiesForEmpl = RandomGenerator.GeneratePermutation(employeesCount);
            wishlists.Add
            (
                new Wishlist
                (
                    forEmpl, 
                    prioritiesForEmpl.Select(ind => ofEmpls.ElementAt(ind-1)).ToArray()
                )
            );
        }

        return wishlists;
    }
}

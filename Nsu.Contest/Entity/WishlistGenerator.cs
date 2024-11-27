namespace Nsu.Contest.Entity;

using Nsu.Contest.Util;

public class WishlistGenerator : IWishlistGenerator
{
    private readonly EntityFactory _entityFactory;
    public WishlistGenerator(EntityFactory entityFactory)
    {
        _entityFactory = entityFactory;
    }

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
                _entityFactory.CreateWishlist
                (
                    forEmpl, 
                    prioritiesForEmpl.Select(ind => ofEmpls.ElementAt(ind-1)).ToArray().Select(e => e.Id).ToArray()
                )
            );
        }

        return wishlists;
    }
}

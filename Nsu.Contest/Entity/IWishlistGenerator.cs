namespace Nsu.Contest.Entity    ;

public interface IWishlistGenerator
{
    IEnumerable<Wishlist> GenerateWishlists(IEnumerable<Employee> forEmpls, IEnumerable<Employee> ofEmpls);
}

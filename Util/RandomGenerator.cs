namespace Nsu.Contest.Util;

using Nsu.Contest.Entity;

public class RandomGenerator
{
    public int[] GenerateRandomPermutation(int n)
    {
        var numbers = Enumerable.Range(1, n).ToArray();
        var rand = new Random();
        for (var i = numbers.Length - 1; i > 0; i--)
        {
            var j = rand.Next(i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }

        return numbers;
    }

    public List<Wishlist> GenerateWishlists(int n)
    {
        var wishlists = new List<Wishlist>(n);
        for (var i = 1; i <= n; ++i)
        {
            var prioritiesForEmpl = GenerateRandomPermutation(n);
            wishlists.Add(new Wishlist(i, prioritiesForEmpl));
        }
        return wishlists;
    }
}

namespace Nsu.Contest.Util;

using System;
using System.Text;
using Nsu.Contest.Entity;

public class RandomGenerator
{
    public static int[] GeneratePermutation(int n)
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

    public static string GenerateRandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        Random random = new Random();
        StringBuilder result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(chars.Length);
            result.Append(chars[index]);
        }

        return result.ToString();
    }
}

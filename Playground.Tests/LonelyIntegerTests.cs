namespace Playground.Tests;

public class LonelyIntegerTests
{
    /// <summary>
    /// Finds the lonely integer using a dictionary to count occurrences.
    /// Time: O(n), Space: O(n). Works for any number of duplicates.
    /// Throws InvalidOperationException if no lonely integer exists.
    /// </summary>
    public int lonelyIntegerWithDictionary(List<int> a)
    {
        var occurrences = new Dictionary<int, int>();
        foreach (int item in a)
        {
            if (occurrences.ContainsKey(item)) occurrences[item]++;
            else
            {
                occurrences.Add(item, 1);
            }
        }
        var lonely = occurrences.First(pair => pair.Value == 1);
        return lonely.Key;
    }

    /// <summary>
    /// Finds the lonely integer using XOR.
    /// Time: O(n), Space: O(1). Assumes exactly one element occurs once, all others occur twice.
    /// </summary>
    public int lonelyIntegerWithXor(List<int> a)
    {
        int result = 0;
        foreach (int num in a)
        {
            result ^= num;
        }
        return result;
    }

    // For backward compatibility, keep the original method name as a wrapper for the dictionary approach.
    public int lonelyInteger(List<int> a) => lonelyIntegerWithDictionary(a);

    [Theory]
    [MemberData(nameof(LonelyIntegerTestData))]
    public void OccursOnce(List<int> a, int i)
    {
        Assert.Equal(lonelyInteger(a), i);
    }

    [Fact]
    public void NoLonelyInteger_ThrowsInvalidOperationException()
    {
        List<int> a = [1, 2, 3, 3, 2, 1];
        Assert.Throws<InvalidOperationException>(() => lonelyInteger(a));
    }

    [Fact]
    public void EmptyArray_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => lonelyInteger([]));
    }

    public static IEnumerable<object[]> LonelyIntegerTestData =>
        [
            [new List<int> { 1, 2, 3, 4, 3, 2, 1}, 4]
        ];
}
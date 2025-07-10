using Xunit.Abstractions;

public class CountingSortTests
{
    private readonly ITestOutputHelper _output;

    public CountingSortTests(ITestOutputHelper output)
    {
        _output = output;
    }

    // 9 8 7 3 5
    // -------------------
    // 0 1 2 3 4 5 6 7 8 9
    // -------------------
    // 0 0 0 1 0 5 0 1 1 1
    /// <summary>
    /// Performs counting sort by creating a frequency array.
    /// Time: O(n + k), Space: O(k) where k is the range of values.
    /// </summary>
    /// <param name="arr">Input array to count frequencies for</param>
    /// <param name="n">Range of values (0 to n-1)</param>
    /// <returns>Frequency array where index represents the value and value represents count</returns>
    private static List<int> countingSort(List<int> arr, int n = 100)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));

        var sorted = new List<int>(new int[n]);
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] >= 0 && arr[i] < n) // Add bounds checking
            {
                sorted[arr[i]]++;
            }
        }
        return sorted;
    }

    /// <summary>
    /// High-performance counting sort using arrays instead of Lists.
    /// Slightly faster due to reduced overhead.
    /// </summary>
    private static int[] countingSortOptimized(List<int> arr, int n = 100)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));

        var frequencies = new int[n];
        foreach (int value in arr)
        {
            if (value >= 0 && value < n)
            {
                frequencies[value]++;
            }
        }
        return frequencies;
    }

    [Fact]
    public void ReturnsFrequencyArray()
    {
        var input = "9 8 7 3 5 0 0 0 0 0";
        var arr = input.Split(" ").Select(element =>
        {
            int.TryParse(element, out int number);
            return number;
        }).ToList();

        var sorted = countingSort(arr, 10);

        var expected = "5 0 0 1 0 1 0 1 1 1";
        Assert.Equal(expected, string.Join(" ", sorted));
    }

    [Fact]
    public void ReturnsFrequencyArray_HackerRankData()
    {
        var input = "63 54 17 78 43 70 32 97 16 94 74 18 60 61 35 83 13 56 75 52 70 12 24 37 17 0 16 64 34 81 82 24 69 2 30 61 83 37 97 16 70 53 0 61 12 17 97 67 33 30 49 70 11 40 67 94 84 60 35 58 19 81 16 14 68 46 42 81 75 87 13 84 33 34 14 96 7 59 17 98 79 47 71 75 8 27 73 66 64 12 29 35 80 78 80 6 5 24 49 82";
        var arr = input.Split(" ").Select(element =>
        {
            int.TryParse(element, out int number);
            return number;
        }).ToList();

        var sorted = countingSort(arr);

        var expected = "2 0 1 0 0 1 1 1 1 0 0 1 3 2 2 0 4 4 1 1 0 0 0 0 3 0 0 1 0 1 2 0 1 2 2 3 0 2 0 0 1 0 1 1 0 0 1 1 0 2 0 0 1 1 1 0 1 0 1 1 2 3 0 1 2 0 1 2 1 1 4 1 0 1 1 3 0 0 2 1 2 3 2 2 2 0 0 1 0 0 0 0 0 0 2 0 1 3 1 0";
        Assert.Equal(expected, string.Join(" ", sorted));
    }
}
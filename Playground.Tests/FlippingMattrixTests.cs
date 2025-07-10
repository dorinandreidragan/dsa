using Xunit.Abstractions;

public class FlippingMattrixTests
{
    private readonly ITestOutputHelper _output;

    /// <summary>
    /// Finds the maximum sum by selecting the best values from each group of 4 positions
    /// that can be moved to the same location through row/column flips.
    /// Time: O(n²), Space: O(1) where n is half the matrix dimension.
    /// </summary>
    /// <param name="matrix">2n×2n matrix to process</param>
    /// <returns>Maximum possible sum of top-left n×n submatrix after flips</returns>
    private static int getMaxSum(List<List<int>> matrix)
    {
        if (matrix == null || matrix.Count == 0 || matrix.Count % 2 != 0)
            throw new ArgumentException("Matrix must be non-null, non-empty, and have even dimensions");

        int n = matrix.Count / 2;
        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // Find max among the 4 positions that can end up at (i,j)
                int val1 = matrix[i][j];                           // original
                int val2 = matrix[2 * n - 1 - i][j];              // flip column
                int val3 = matrix[i][2 * n - 1 - j];              // flip row
                int val4 = matrix[2 * n - 1 - i][2 * n - 1 - j];  // flip both

                sum += Math.Max(Math.Max(val1, val2), Math.Max(val3, val4));
            }
        }

        return sum;
    }

    /// <summary>
    /// Ultra-optimized version using bit manipulation for finding max of 4 values.
    /// Slightly faster for competitive programming scenarios.
    /// </summary>
    private static int getMaxSumOptimized(List<List<int>> matrix)
    {
        if (matrix == null || matrix.Count == 0 || matrix.Count % 2 != 0)
            throw new ArgumentException("Matrix must be non-null, non-empty, and have even dimensions");

        int n = matrix.Count / 2;
        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // Get all 4 values that can end up at position (i,j)
                int val1 = matrix[i][j];
                int val2 = matrix[2 * n - 1 - i][j];
                int val3 = matrix[i][2 * n - 1 - j];
                int val4 = matrix[2 * n - 1 - i][2 * n - 1 - j];

                // Find maximum using nested ternary (fastest for 4 values)
                sum += val1 > val2 ? (val1 > val3 ? (val1 > val4 ? val1 : val4) : (val3 > val4 ? val3 : val4))
                                   : (val2 > val3 ? (val2 > val4 ? val2 : val4) : (val3 > val4 ? val3 : val4));
            }
        }

        return sum;
    }

    public FlippingMattrixTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void GetMaxSum_ReturnsMaxSum()
    {
        List<List<int>> matrix = [
            [50, 102, 43, 99],
            [30, 32, 54, 109],
            [45, 47, 53, 125],
            [75, 114, 23, 22],
        ];

        // [99, 114]
        // [125, 54]
        // sum = 99 + 114 + 125 + 54 = 392

        var sum = getMaxSum(matrix);

        Assert.Equal(392, sum);
    }

    [Fact]
    public void GetMaxSum_EdgeCase_2x2Matrix()
    {
        List<List<int>> matrix = [
            [1, 2],
            [3, 4]
        ];

        // Only one position (0,0), max of [1,3,2,4] = 4
        var sum = getMaxSum(matrix);
        Assert.Equal(4, sum);
    }

    [Fact]
    public void GetMaxSum_InvalidInput_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => getMaxSum(null!));
        Assert.Throws<ArgumentException>(() => getMaxSum(new List<List<int>>()));

        // Odd dimensions should fail
        List<List<int>> oddMatrix = [
            [1, 2, 3],
            [4, 5, 6],
            [7, 8, 9]
        ];
        Assert.Throws<ArgumentException>(() => getMaxSum(oddMatrix));
    }

    [Fact]
    public void GetMaxSum_CompareBothImplementations()
    {
        List<List<int>> matrix = [
            [50, 102, 43, 99],
            [30, 32, 54, 109],
            [45, 47, 53, 125],
            [75, 114, 23, 22],
        ];

        var sum1 = getMaxSum(matrix);
        var sum2 = getMaxSumOptimized(matrix);

        Assert.Equal(sum1, sum2);
        Assert.Equal(392, sum1);
    }
}
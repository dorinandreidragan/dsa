public class PairsTests
{
    /// <summary>
    /// Counts pairs of integers in an array that have a specific difference k.
    /// Uses a HashSet for O(1) lookup time to check if num + k exists.
    /// Time: O(n) for HashSet creation + O(n) for iteration = O(n)
    /// Space: O(n) for HashSet storage
    /// </summary>
    /// <param name="k">The target difference between pairs</param>
    /// <param name="arr">Array of integers to search for pairs</param>
    /// <returns>Count of pairs where arr[j] - arr[i] = k</returns>
    public int pairs(int k, List<int>? arr)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));
        if (k < 0) throw new ArgumentException("Difference k must be non-negative", nameof(k));

        // Create HashSet for O(1) contains operations
        var numSet = new HashSet<int>(arr);

        // For each number, check if number + k exists in the set
        // This gives us pairs where the difference is exactly k
        return arr.Count(num => numSet.Contains(num + k));
    }

    /// <summary>
    /// Two-pointer approach for finding pairs with difference k.
    /// Sorts the array first, then uses two pointers to find pairs efficiently.
    /// Time: O(n log n) due to sorting + O(n) for two-pointer traversal = O(n log n)
    /// Space: O(1) for the actual algorithm (excluding sorting space)
    /// </summary>
    /// <param name="k">The target difference between pairs</param>
    /// <param name="arr">Array of integers to search for pairs</param>
    /// <returns>Count of pairs where arr[j] - arr[i] = k</returns>
    public int pairsTwoPointers(int k, List<int>? arr)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));
        if (k < 0) throw new ArgumentException("Difference k must be non-negative", nameof(k));
        if (arr.Count < 2) return 0;

        // Create a copy to avoid modifying the original array
        var sortedArr = new List<int>(arr);
        sortedArr.Sort();

        int left = 0;
        int right = 1;
        int count = 0;

        while (right < sortedArr.Count)
        {
            int diff = sortedArr[right] - sortedArr[left];

            if (diff == k)
            {
                // Found a valid pair
                count++;

                // Skip duplicates for the left pointer
                int currentLeft = sortedArr[left];
                while (left < sortedArr.Count && sortedArr[left] == currentLeft)
                {
                    left++;
                }

                // Update right pointer to maintain left < right
                right = Math.Max(right + 1, left + 1);
            }
            else if (diff < k)
            {
                // Difference is too small, move right pointer to increase difference
                right++;
            }
            else
            {
                // Difference is too large, move left pointer to decrease difference
                left++;

                // Ensure left < right
                if (left == right)
                {
                    right++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// Alternative brute force approach for comparison.
    /// Time: O(n²), Space: O(1)
    /// Less efficient but more intuitive for understanding the problem.
    /// </summary>
    public int pairsBruteForce(int k, List<int>? arr)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));
        if (k < 0) throw new ArgumentException("Difference k must be non-negative", nameof(k));

        int count = 0;
        for (int i = 0; i < arr.Count; i++)
        {
            for (int j = i + 1; j < arr.Count; j++)
            {
                // Check if the absolute difference equals k
                if (Math.Abs(arr[j] - arr[i]) == k)
                {
                    count++;
                }
            }
        }
        return count;
    }

    [Theory]
    [MemberData(nameof(PairsTestData))]
    public void Pairs_OptimizedSolution_ReturnsCorrectCount(int k, List<int> arr, int expected)
    {
        Assert.Equal(expected, pairs(k, arr));
    }

    [Theory]
    [MemberData(nameof(PairsTestData))]
    public void Pairs_TwoPointersSolution_ReturnsCorrectCount(int k, List<int> arr, int expected)
    {
        Assert.Equal(expected, pairsTwoPointers(k, arr));
    }

    [Theory]
    [MemberData(nameof(PairsTestData))]
    public void Pairs_BruteForceSolution_ReturnsCorrectCount(int k, List<int> arr, int expected)
    {
        Assert.Equal(expected, pairsBruteForce(k, arr));
    }

    [Fact]
    public void Pairs_TwoPointers_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => pairsTwoPointers(1, null));
    }

    [Fact]
    public void Pairs_TwoPointers_NegativeDifference_ThrowsArgumentException()
    {
        var arr = new List<int> { 1, 2, 3 };
        Assert.Throws<ArgumentException>(() => pairsTwoPointers(-1, arr));
    }

    [Fact]
    public void Pairs_TwoPointers_EmptyArray_ReturnsZero()
    {
        var result = pairsTwoPointers(1, new List<int>());
        Assert.Equal(0, result);
    }

    [Fact]
    public void Pairs_TwoPointers_SingleElement_ReturnsZero()
    {
        var result = pairsTwoPointers(1, new List<int> { 5 });
        Assert.Equal(0, result);
    }

    [Fact]
    public void Pairs_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => pairs(1, null));
    }

    [Fact]
    public void Pairs_NegativeDifference_ThrowsArgumentException()
    {
        var arr = new List<int> { 1, 2, 3 };
        Assert.Throws<ArgumentException>(() => pairs(-1, arr));
    }

    [Fact]
    public void Pairs_EmptyArray_ReturnsZero()
    {
        var result = pairs(1, new List<int>());
        Assert.Equal(0, result);
    }

    [Fact]
    public void Pairs_SingleElement_ReturnsZero()
    {
        var result = pairs(1, new List<int> { 5 });
        Assert.Equal(0, result);
    }

    [Fact]
    public void Pairs_DuplicateNumbers_CountsCorrectly()
    {
        // Array: [1, 1, 3, 3], k = 2
        // Pairs: (1,3) - even with duplicates, we count unique pairs
        var result = pairs(2, new List<int> { 1, 1, 3, 3 });
        Assert.Equal(2, result); // Both 1s can pair with both 3s
    }

    public static IEnumerable<object[]> PairsTestData =>
    [
        // Basic test cases
        [1, new List<int> {1, 2, 3, 4}, 3], // Pairs: (1,2), (2,3), (3,4)
        [2, new List<int> {1, 5, 3, 4, 2}, 3], // Pairs: (1,3), (2,4), (3,5)
        
        // Edge cases
        [5, new List<int> {1, 2, 3, 4, 5, 6}, 1], // Only pair: (1,6)
        [10, new List<int> {1, 2, 3}, 0], // No pairs with difference 10
        
        // Larger test case
        [2, new List<int> {1, 3, 5, 8, 6, 4, 2}, 5], // Pairs: (1,3), (2,4), (3,5), (4,6), (6,8)
        
        // Unordered array
        [3, new List<int> {4, 1, 7, 10}, 3], // Pairs: (1,4), (7,10), (7,4)
    ];
}
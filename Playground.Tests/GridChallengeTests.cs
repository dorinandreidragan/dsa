using System.ComponentModel;
using System.Data;

namespace Playground.Tests;

public class GridChallengeTests
{

    /// <summary>
    /// Determines if a grid can be rearranged so that both rows and columns are sorted.
    /// First sorts each row alphabetically, then checks if all columns are non-decreasing.
    /// Handles both square and rectangular grids (n×m matrices).
    /// Time: O(n * m log m), Space: O(1) where n = rows, m = characters per row.
    /// </summary>
    /// <param name="grid">Grid of strings representing character matrix (can be rectangular)</param>
    /// <returns>"YES" if grid can be arranged with sorted rows and columns, "NO" otherwise</returns>
    public string gridChallenge(List<string> grid)
    {
        // Sort each row alphabetically
        for (int i = 0; i < grid.Count; i++)
        {
            var characters = grid[i].ToCharArray();
            Array.Sort(characters);
            grid[i] = new string(characters);
        }

        // Verify all columns are in non-decreasing order
        int noOfRows = grid.Count;
        int noOfCols = grid[0].Length; // Handle rectangular grids (n×m)
        for (int col = 0; col < noOfCols; col++)
        {
            for (int row = 1; row < noOfRows; row++)
            {
                // If any column has decreasing characters, return NO
                if (grid[row][col] < grid[row - 1][col])
                {
                    return "NO";
                }
            }
        }

        return "YES";
    }

    [Theory]
    [MemberData(nameof(GridChallengeData))]
    public void GridChallenge_TestCases(List<string> grid, string expected)
    {
        Assert.Equal(expected, gridChallenge(grid));
    }

    public static IEnumerable<object[]> GridChallengeData =>
        [
            // Single character grid - always possible
            [new List<string> {"a"}, "YES"],
            
            // Simple impossible case - can't arrange columns properly
            [new List<string> {"z", "y", "x"}, "NO"],
            
            // Already sorted grid
            [new List<string> {"abc", "ade", "efg"}, "YES"],
            
            // Grid that needs row sorting but columns work
            [new List<string> {"cba", "fed", "ihg"}, "YES"],

            // Larger grid that doesn't work
            [new List<string> {"mpxz", "abcd", "wlmf"}, "NO"],
            
            // Larger grid that works
            [new List<string> {"ebacd", "fghij", "olmkn", "trpqs", "xywuv"}, "YES"],
            
            // Edge case: repeated characters
            [new List<string> {"aaa", "bbb", "ccc"}, "YES"],
            
            // Mixed case that fails
            [new List<string> {"zyx", "abc", "def"}, "NO"]
        ];
}
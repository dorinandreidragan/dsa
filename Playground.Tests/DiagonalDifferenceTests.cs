
namespace Playground.Tests;

public class DiagonalDifferenceTests
{
    public int diagonalDifference(List<List<int>> arr)
    {
        var firstDiagonalSum = 0;
        var secondDiagonalSum = 0;
        var n = arr.Count;

        for (int i = 0; i < n; i++)
        {
            firstDiagonalSum += arr[i][i];
            secondDiagonalSum += arr[i][n - 1 - i];
        }

        return Math.Abs(firstDiagonalSum - secondDiagonalSum);
    }

    [Fact]
    public void DiagonalDifference()
    {
        List<List<int>> arr = [
            [1, 2, 3],
            [4, 5, 6],
            [9, 8, 9]
        ];

        Assert.Equal(2, diagonalDifference(arr));
    }

}
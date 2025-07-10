public class TowerBreakersTests
{
    /// <summary>
    /// Determines the winner of the Tower Breakers game using game theory.
    /// 
    /// Game Rules:
    /// - Two players alternate turns
    /// - On each turn, a player reduces a tower of height X to height Y (where Y < X and Y divides X evenly)
    /// - The player who cannot make a move loses
    /// 
    /// Key Insights:
    /// 1. If m=1, no moves possible → Player 2 wins
    /// 2. If n is even, Player 2 can mirror Player 1's moves → Player 2 wins
    /// 3. If n is odd and m>1, Player 1 can force a win by breaking symmetry
    /// 
    /// Time: O(1), Space: O(1)
    /// </summary>
    /// <param name="n">Number of towers</param>
    /// <param name="m">Initial height of each tower</param>
    /// <returns>1 if Player 1 wins, 2 if Player 2 wins</returns>
    public static int towerBreakers(int n, int m)
    {
        // Edge case: No moves possible (all towers have height 1)
        if (m == 1)
        {
            return 2; // Player 2 wins (Player 1 cannot move)
        }

        // Even number of towers: Player 2 can mirror Player 1's strategy
        if (n % 2 == 0)
        {
            return 2; // Player 2 wins by mirroring
        }

        // Odd number of towers with height > 1: Player 1 wins
        return 1; // Player 1 can break symmetry and force a win
    }

    // Keep the original method name for backward compatibility
    public int towerBrakers(int n, int m) => towerBreakers(n, m);

    [Theory]
    [InlineData(1, 1, 2)] // No moves possible for P1
    [InlineData(2, 1, 2)] // No moves possible for P1
    [InlineData(2, 6, 2)] // P2 mirrors P1 moves and wins (even towers)
    [InlineData(3, 6, 1)] // P1 wins breaking symmetry (odd towers)
    [InlineData(4, 10, 2)] // Even towers, P2 mirrors
    [InlineData(1, 100, 1)] // Single tower with height > 1, P1 wins
    [InlineData(100, 1, 2)] // Many towers but height 1, P2 wins
    [InlineData(101, 50, 1)] // Odd towers with moves possible, P1 wins
    public void GetWinnerTest(int n, int m, int winner)
    {
        Assert.Equal(winner, towerBrakers(n, m));
        Assert.Equal(winner, towerBreakers(n, m)); // Test both methods
    }

    [Fact]
    public void GameTheory_EdgeCases()
    {
        // Test the mathematical reasoning behind the solution

        // Case 1: No moves available
        Assert.Equal(2, towerBreakers(1000000, 1)); // P2 wins regardless of tower count

        // Case 2: Even towers - mirroring strategy
        Assert.Equal(2, towerBreakers(2, 1000000)); // P2 can mirror any move
        Assert.Equal(2, towerBreakers(1000, 50)); // Large even number

        // Case 3: Odd towers with moves - first player advantage
        Assert.Equal(1, towerBreakers(1, 1000000)); // Single tower, P1 makes last move
        Assert.Equal(1, towerBreakers(999, 50)); // Large odd number
    }

    /// <summary>
    /// Demonstrates why the O(1) solution works through game theory analysis.
    /// This method explains the mathematical reasoning but is not needed for the actual solution.
    /// </summary>
    /// <param name="n">Number of towers</param>
    /// <param name="m">Initial height</param>
    /// <returns>Detailed explanation of why player wins</returns>
    public static string explainStrategy(int n, int m)
    {
        if (m == 1)
        {
            return $"Player 2 wins: All {n} towers have height 1, no moves possible for Player 1.";
        }

        if (n % 2 == 0)
        {
            return $"Player 2 wins: With {n} towers (even), Player 2 can mirror every move Player 1 makes. " +
                   "When Player 1 reduces tower X to height Y, Player 2 reduces another tower to the same height Y. " +
                   "This mirroring strategy guarantees Player 2 makes the last move.";
        }

        return $"Player 1 wins: With {n} towers (odd) and height {m} > 1, Player 1 can break symmetry. " +
               "Player 1 makes the first move, leaving an even number of 'changed' towers. " +
               "Player 2 cannot maintain perfect symmetry, so Player 1 can force a winning position.";
    }

    [Fact]
    public void ExplainStrategy_Test()
    {
        // Verify our explanations make sense
        string explanation1 = explainStrategy(2, 6);
        Assert.Contains("mirror", explanation1.ToLower());

        string explanation2 = explainStrategy(3, 6);
        Assert.Contains("break symmetry", explanation2.ToLower());

        string explanation3 = explainStrategy(100, 1);
        Assert.Contains("no moves possible", explanation3.ToLower());
    }
}
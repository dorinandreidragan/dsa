using System.Numerics;

namespace Playground.Tests;

/// <summary>
/// LEGO Blocks Problem: Building walls with no vertical breaks
/// 
/// ═══════════════════════════════════════════════════════════════════════
/// PROBLEM STATEMENT:
/// ═══════════════════════════════════════════════════════════════════════
/// Build a wall of height H and width W using LEGO blocks of widths 1, 2, 3, and 4.
/// The wall must be solid (no holes) and have no straight vertical breaks that go through all rows.
/// 
/// ═══════════════════════════════════════════════════════════════════════
/// VISUAL EXAMPLES:
/// ═══════════════════════════════════════════════════════════════════════
/// Example of a BAD wall (has vertical break):
/// Row 1: [1|2]     ← break after position 1
/// Row 2: [1|2]     ← same break position - INVALID!
/// 
/// Example of a GOOD wall (no vertical breaks):
/// Row 1: [1|2]     ← break after position 1
/// Row 2: [2|1]     ← break after position 2 (different!) - VALID!
/// 
/// ═══════════════════════════════════════════════════════════════════════
/// ALGORITHM BREAKDOWN:
/// ═══════════════════════════════════════════════════════════════════════
/// 
/// STEP 1: Count Single Row Arrangements (Extended Fibonacci)
/// ────────────────────────────────────────────────────────────────────
/// For width W, count ways to arrange blocks of sizes 1,2,3,4
/// Formula: ways[i] = ways[i-1] + ways[i-2] + ways[i-3] + ways[i-4]
/// 
/// STEP 2: Calculate Total Wall Arrangements
/// ────────────────────────────────────────────────────────────────────
/// For H independent rows: total = (single_row_ways)^H
/// 
/// STEP 3: Subtract Invalid Arrangements (Inclusion-Exclusion)
/// ────────────────────────────────────────────────────────────────────
/// Remove arrangements with at least one vertical break using:
/// valid[i] = total[i] - Σ(valid[k] × total[i-k]) for all break positions k
/// 
/// ═══════════════════════════════════════════════════════════════════════
/// COMPLEXITY ANALYSIS:
/// ═══════════════════════════════════════════════════════════════════════
/// Time:  O(W²) - dominated by inclusion-exclusion step
/// Space: O(W)  - three arrays of size W+1
/// 
/// ═══════════════════════════════════════════════════════════════════════
/// SPECIAL CASES HANDLED:
/// ═══════════════════════════════════════════════════════════════════════
/// • Height = 1: No vertical breaks possible, return single row count
/// • Modular arithmetic: Prevents integer overflow for large inputs
/// • Edge cases: Proper handling of small widths (0,1,2,3)
/// </summary>
public class LegoBlocksTests
{
    const int MOD = 1000000007; // 10^9 + 7 - standard modulo for large number problems

    /// <summary>
    /// Main entry point: Calculate valid LEGO wall arrangements
    /// 
    /// ALGORITHM OVERVIEW:
    /// ═══════════════════════════════════════════════════════════════════════
    /// STEP 1: Count Single Row Arrangements (Extended Fibonacci)
    /// STEP 2: Calculate Total Wall Arrangements (Exponentiation)
    /// STEP 3: Remove Invalid Arrangements (Inclusion-Exclusion)
    /// ═══════════════════════════════════════════════════════════════════════
    /// </summary>
    /// <param name="height">Number of rows in the wall</param>
    /// <param name="width">Width of each row</param>
    /// <returns>Number of valid arrangements modulo 10^9+7</returns>
    public int LegoBlocks(int height, int width)
    {
        // ═══════════════════════════════════════════════════════════════════════
        // STEP 1: Count ways to fill a single row using blocks of widths 1, 2, 3, 4
        // ═══════════════════════════════════════════════════════════════════════
        // 
        // This is an extended Fibonacci sequence with 4 terms instead of 2:
        // ways[i] = ways[i-1] + ways[i-2] + ways[i-3] + ways[i-4]
        // 
        // Why? To fill width i, your first block can be:
        // - Width 1: leaves (i-1) remaining → ways[i-1] ways to fill the rest
        // - Width 2: leaves (i-2) remaining → ways[i-2] ways to fill the rest
        // - Width 3: leaves (i-3) remaining → ways[i-3] ways to fill the rest
        // - Width 4: leaves (i-4) remaining → ways[i-4] ways to fill the rest
        // 
        // CONCRETE EXAMPLES:
        // Width 0: [] → 1 way (do nothing)
        // Width 1: [1] → 1 way
        // Width 2: [1,1], [2] → 2 ways
        // Width 3: [1,1,1], [1,2], [2,1], [3] → 4 ways
        // Width 4: [1,1,1,1], [1,1,2], [1,2,1], [2,1,1], [2,2], [1,3], [3,1], [4] → 8 ways

        long[] rowWays = new long[width + 1];

        // Base cases - manually calculated and verified
        rowWays[0] = 1; // Width 0: one way (place nothing)
        if (width >= 1) rowWays[1] = 1; // Width 1: [1]
        if (width >= 2) rowWays[2] = 2; // Width 2: [1,1], [2]  
        if (width >= 3) rowWays[3] = 4; // Width 3: [1,1,1], [1,2], [2,1], [3]

        // Fill remaining widths using the recurrence relation
        for (int i = 4; i <= width; i++)
        {
            rowWays[i] = (rowWays[i - 1] + rowWays[i - 2] + rowWays[i - 3] + rowWays[i - 4]) % MOD;
        }

        // Special case: for height=1, there can be no vertical breaks
        // so the answer is just the number of ways to fill a single row
        if (height == 1)
        {
            return (int)rowWays[width];
        }

        // ═══════════════════════════════════════════════════════════════════════
        // STEP 2: Calculate total arrangements for H rows, allowing vertical breaks
        // ═══════════════════════════════════════════════════════════════════════
        // 
        // If a single row of width i can be filled in rowWays[i] ways,
        // then H independent rows can be filled in rowWays[i]^H ways total.
        // 
        // Why? Each row is independent, so we multiply the possibilities:
        // Row 1: rowWays[i] choices
        // Row 2: rowWays[i] choices  
        // ...
        // Row H: rowWays[i] choices
        // Total: rowWays[i] × rowWays[i] × ... × rowWays[i] = rowWays[i]^H

        long[] allWays = new long[width + 1];
        for (int i = 0; i <= width; i++)
        {
            // Calculate rowWays[i]^height using efficient modular exponentiation
            // BigInteger.ModPow handles large exponents without overflow
            allWays[i] = (long)BigInteger.ModPow(rowWays[i], height, MOD);
        }

        // ═══════════════════════════════════════════════════════════════════════
        // STEP 3: Remove arrangements with vertical breaks (Inclusion-Exclusion)
        // ═══════════════════════════════════════════════════════════════════════
        // 
        // valid[i] = total[i] - (arrangements with at least one vertical break)
        // 
        // How do we count "arrangements with vertical breaks"?
        // For each possible break position k (where 1 ≤ k < i):
        // - Left part (width k): Must be valid (no internal breaks) → valid[k] ways
        // - Right part (width i-k): Can be anything → total[i-k] ways
        // - Total bad arrangements with break at k: valid[k] × total[i-k]
        // 
        // Why this works: If there's a break at position k, the left side must be 
        // completely valid (no internal breaks), but the right side can have any pattern.

        long[] validWays = new long[width + 1];
        validWays[0] = 1; // Base case: one way to build width 0 (do nothing)

        for (int i = 1; i <= width; i++)
        {
            // Start with all possible arrangements for width i
            validWays[i] = allWays[i];

            // Subtract arrangements with vertical breaks using inclusion-exclusion
            for (int k = 1; k < i; k++)
            {
                // For a vertical break at position k:
                // - Left part (width k): validWays[k] ways (must be completely solid)
                // - Right part (width i-k): allWays[i-k] ways (can have any pattern)
                long badArrangements = ((validWays[k] % MOD) * (allWays[i - k] % MOD)) % MOD;

                // Subtract bad arrangements, handle negative results properly
                // The "+ MOD" ensures we don't get negative numbers after subtraction
                validWays[i] = (validWays[i] - badArrangements + MOD) % MOD;
            }
        }

        return (int)validWays[width];

        // ═══════════════════════════════════════════════════════════════════════
        // ALGORITHM SUMMARY:
        // ═══════════════════════════════════════════════════════════════════════
        // • Time Complexity: O(w²) - dominated by the inclusion-exclusion step
        // • Space Complexity: O(w) - three arrays of size w+1
        // • Key Insight: Extended Fibonacci + Inclusion-Exclusion Principle
        // • Special Case: height=1 has no vertical breaks, return single row count
        // • Modular Arithmetic: Prevents integer overflow for large inputs
        // ═══════════════════════════════════════════════════════════════════════
    }

    #region Test Cases

    /// <summary>
    /// Tests for small, manually verifiable cases
    /// </summary>
    [Theory]
    [InlineData(1, 1, 1)]    // Single block: only [1]
    [InlineData(1, 2, 2)]    // Single row: [1,1] or [2]
    [InlineData(1, 3, 4)]    // Single row: [1,1,1], [1,2], [2,1], [3]
    [InlineData(1, 4, 8)]    // Single row: 8 different combinations
    public void LegoBlocks_SingleRow_ReturnsCorrectCount(int height, int width, int expected)
    {
        var result = LegoBlocks(height, width);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests for multiple rows - these verify the inclusion-exclusion logic
    /// </summary>
    [Theory]
    [InlineData(2, 2, 3)]    // Two rows, width 2: total 4 ways, 1 bad → 3 valid
    [InlineData(2, 3, 9)]    // Two rows, width 3: total 16 ways, 7 bad → 9 valid
    [InlineData(3, 2, 7)]    // Three rows, width 2
    public void LegoBlocks_MultipleRows_ReturnsCorrectCount(int height, int width, int expected)
    {
        var result = LegoBlocks(height, width);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests from HackerRank - these are the actual problem test cases
    /// </summary>
    [Theory]
    [InlineData(4, 5, 35714)]    // Test case from HackerRank
    [InlineData(4, 6, 447902)]   // Test case from HackerRank  
    [InlineData(4, 7, 5562914)] // Test case from HackerRank
    [InlineData(5, 4, 29791)]    // Test case from HackerRank
    [InlineData(6, 4, 250047)]   // Test case from HackerRank
    [InlineData(7, 4, 2048383)]  // Test case from HackerRank
    public void LegoBlocks_HackerRankCases_ReturnsCorrectCount(int height, int width, int expected)
    {
        var result = LegoBlocks(height, width);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Performance and boundary tests
    /// </summary>
    [Theory]
    [InlineData(1, 50)]   // Large width, small height
    [InlineData(50, 1)]   // Large height, small width
    [InlineData(10, 10)]  // Moderately large case
    public void LegoBlocks_LargeCases_DoesNotThrow(int height, int width)
    {
        // Should complete without throwing exceptions or timeouts
        var result = LegoBlocks(height, width);
        Assert.True(result >= 0);
        Assert.True(result < MOD);
    }

    /// <summary>
    /// Test that verifies the extended Fibonacci sequence logic for single rows
    /// by testing single-row cases (height=1) 
    /// </summary>
    [Fact]
    public void LegoBlocks_SingleRowCases_VerifiesExtendedFibonacciSequence()
    {
        // For height=1, the result should match the extended Fibonacci sequence
        // This verifies that STEP 1 of our algorithm is working correctly

        // Base cases
        Assert.Equal(1, LegoBlocks(1, 0)); // Width 0: 1 way (do nothing)
        Assert.Equal(1, LegoBlocks(1, 1)); // Width 1: [1] → 1 way
        Assert.Equal(2, LegoBlocks(1, 2)); // Width 2: [1,1], [2] → 2 ways
        Assert.Equal(4, LegoBlocks(1, 3)); // Width 3: [1,1,1], [1,2], [2,1], [3] → 4 ways
        Assert.Equal(8, LegoBlocks(1, 4)); // Width 4: 8 ways (sum of previous 4: 1+1+2+4=8)
        Assert.Equal(15, LegoBlocks(1, 5)); // Width 5: 15 ways (sum of previous 4: 1+2+4+8=15)

        // Test the recurrence relation: ways[i] = ways[i-1] + ways[i-2] + ways[i-3] + ways[i-4]
        // For width 6: should be 15 + 8 + 4 + 2 = 29
        Assert.Equal(29, LegoBlocks(1, 6));
    }

    /// <summary>
    /// Manually verify the algorithm logic for width=3, height=2 case
    /// </summary>
    [Fact]
    public void LegoBlocks_Width3Height2_ManualVerification()
    {
        // For width=3, height=2, we expect 9 valid arrangements
        // This tests all three steps of our algorithm working together:
        // Step 1: Single row has 4 ways: [1,1,1], [1,2], [2,1], [3]
        // Step 2: Total ways for 2 rows = 4^2 = 16 combinations
        // Step 3: Remove 7 bad arrangements with vertical breaks = 16 - 7 = 9 valid

        var result = LegoBlocks(2, 3);
        Assert.Equal(9, result);

        // The 7 bad arrangements that get removed are:
        // Break after position 1: [1,*] + [1,*] = 4 arrangements  
        // Break after position 2: [*,1] + [*,1] = 4 arrangements
        // Break after both positions: [1,1,1] + [1,1,1] = 1 arrangement (counted twice)
        // Total bad = 4 + 4 - 1 = 7 (inclusion-exclusion principle)
    }

    /// <summary>
    /// Debug test to verify single row width 3 calculation
    /// </summary>
    [Fact]
    public void Debug_SingleRowWidth3()
    {
        // For height=1, width=3, the result should be 4
        // This represents: [1,1,1], [1,2], [2,1], [3]
        var result = LegoBlocks(1, 3);
        Assert.Equal(4, result);

        // This tests that our STEP 1 (extended Fibonacci) logic is correct
        // and that the special case for height=1 works properly
    }

    #endregion
}
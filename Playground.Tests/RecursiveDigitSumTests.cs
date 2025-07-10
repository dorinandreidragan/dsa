using System.Text;

public class RecursiveDigitSumTests
{
    /// <summary>
    /// Calculates the recursive digit sum (digital root) using the brute force approach.
    /// Concatenates string n exactly k times, then repeatedly sums digits until single digit remains.
    /// Time: O(log(sum) * d), Space: O(log(sum) * d) where d = total digits.
    /// </summary>
    /// <param name="n">String representation of the number</param>
    /// <param name="k">Number of times to concatenate n</param>
    /// <returns>Single digit result after recursive digit summation</returns>
    public static int superDigit(string n, int k)
    {
        // First get the digital root of the original string
        // Then multiply by k and get the digital root of that result
        return superDigitInternal(
            $"{superDigitInternal(n) * k}");
    }

    /// <summary>
    /// Helper method that recursively sums digits until a single digit remains.
    /// Uses string parsing and recursion - inefficient but demonstrates the concept clearly.
    /// </summary>
    /// <param name="n">String representation of number to process</param>
    /// <returns>Single digit digital root</returns>
    public static int superDigitInternal(string n)
    {
        // Base case: if already single digit, return it
        if (n.Length == 1)
            return int.Parse($"{n}");

        // Calculate sum of all digits in current number
        var sum = 0;
        foreach (var digit in n)
        {
            sum += int.Parse($"{digit}"); // Convert char digit to int
        }

        // Recursive call with the sum converted back to string
        return superDigitInternal($"{sum}");
    }

    /// <summary>
    /// Optimized solution using digital root mathematical property.
    /// Uses modular arithmetic to calculate result in O(n) time and O(1) space.
    /// Based on the property: digital_root(n) = 1 + (n-1) % 9 for n > 0.
    /// Time: O(d), Space: O(1) where d = digits in original string.
    /// </summary>
    /// <param name="n">String representation of the number</param>
    /// <param name="k">Number of times to concatenate n</param>
    /// <returns>Single digit result using mathematical optimization</returns>
    public static int superDigit_ModularArithmetic(string n, int k)
    {
        // Calculate sum of digits in original string
        long totalSum = 0;
        foreach (var digit in n)
        {
            totalSum += digit - '0'; // More efficient than int.Parse
        }

        // Multiply by k (equivalent to concatenating k times)
        totalSum *= k;

        // Apply digital root formula using modular arithmetic
        // If divisible by 9: result is 9 (unless original was 0)
        // Otherwise: result is the remainder when divided by 9
        return totalSum % 9 == 0 ?
            (totalSum == 0 ? 0 : 9) :
            (int)(totalSum % 9);
    }

    [Theory]
    [InlineData("1", 1, 1)]                    // Single digit - base case
    [InlineData("11", 1, 2)]                   // Two identical digits: 1+1=2
    [InlineData("56", 1, 2)]                   // Simple case: 5+6=11, 1+1=2
    [InlineData("1234", 1, 1)]                 // Multi-digit: 1+2+3+4=10, 1+0=1
    [InlineData("567", 3, 9)]                  // With multiplication: (5+6+7)*3=54, 5+4=9
    [InlineData("999", 1, 9)]                  // Multiple of 9 case: 9+9+9=27, 2+7=9
    [InlineData("123", 3, 9)]                  // (1+2+3)*3 = 18, 1+8=9
    [InlineData("0", 1, 0)]                    // Edge case: zero
    [InlineData("9", 1, 9)]                    // Single digit 9
    [InlineData("10", 1, 1)]                   // Powers of 10: 1+0=1
    [InlineData("100", 1, 1)]                  // 1+0+0=1
    [InlineData("18", 5, 9)]                   // 18*5=90, 9+0=9
    [InlineData("7404954009694227446246375747227852213692570890717884174001587537145838723390362624487926131161112710589127423098959327020544003395792482625191721603328307774998124389641069884634086849138515079220750462317357487762780480576640689175346956135668451835480490089962406773267569650663927778867764315211280625033388271518264961090111547480467065229843613873499846390257375933040086863430523668050046930387013897062106309406874425001127890574986610018093859693455518413268914361859000614904461902442822577552997680098389183082654625098817411306985010658756762152160904278169491634807464356130877526392725432086439934006728914411061861235300979536190100734360684054557448454640750198466877185875290011114667186730452681943043971812380628117527172389889545776779555664826488520325234792648448625225364535053605515386730925070072896004645416713682004600636574389040662827182696337187610904694029221880801372864040345567230941110986028568372710970460116491983700312243090679537497139499778923997433720159174153", 100000, 3)]  // Large number stress test
    public void SuperDigit_TestCases(string n, int k, int expected)
    {
        // Test both implementations produce the same result
        Assert.Equal(expected, superDigit(n, k));
        Assert.Equal(expected, superDigit_ModularArithmetic(n, k));
    }

    [Fact]
    public void SuperDigit_DigitalRootProperties_Test()
    {
        // Test fundamental digital root properties

        // Property 1: Digital root of 1-9 is the number itself
        for (int i = 1; i <= 9; i++)
        {
            Assert.Equal(i, superDigit_ModularArithmetic(i.ToString(), 1));
        }

        // Property 2: Digital root cycles every 9 numbers
        Assert.Equal(1, superDigit_ModularArithmetic("10", 1)); // 10 -> 1
        Assert.Equal(2, superDigit_ModularArithmetic("11", 1)); // 11 -> 2
        Assert.Equal(1, superDigit_ModularArithmetic("19", 1)); // 19 -> 1

        // Property 3: Multiples of 9 have digital root 9 (except 0)
        Assert.Equal(9, superDigit_ModularArithmetic("9", 1));
        Assert.Equal(9, superDigit_ModularArithmetic("18", 1));
        Assert.Equal(9, superDigit_ModularArithmetic("27", 1));
        Assert.Equal(9, superDigit_ModularArithmetic("99", 1));
    }

    [Theory]
    [InlineData("123", 0, 0)]        // Edge case: k=0 should give 0
    [InlineData("5", 10, 5)]         // Single digit repeated: 5*10=50, 5+0=5
    [InlineData("99", 11, 9)]        // (9+9)*11 = 198, 1+9+8=18, 1+8=9
    public void SuperDigit_EdgeCases(string n, int k, int expected)
    {
        // Only test the optimized version for edge cases involving k=0
        if (k == 0)
        {
            Assert.Equal(expected, superDigit_ModularArithmetic(n, k));
        }
        else
        {
            Assert.Equal(expected, superDigit(n, k));
            Assert.Equal(expected, superDigit_ModularArithmetic(n, k));
        }
    }
}
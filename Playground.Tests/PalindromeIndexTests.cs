using Xunit.Sdk;

public class PalindromeIndexTests
{
    /// <summary>
    /// Finds the index of the character that, when removed, makes the string palindromic.
    /// Uses two-pointer technique for O(n) time complexity.
    /// Returns -1 if string is already palindromic or no single removal works.
    /// 
    /// Algorithm:
    /// 1. Start with pointers at both ends
    /// 2. Move inward while characters match
    /// 3. On mismatch, try removing either left or right character
    /// 4. Validate if removal creates palindrome for entire remaining string
    /// 
    /// Time: O(n), Space: O(1)
    /// </summary>
    /// <param name="s">Input string to analyze</param>
    /// <returns>Index of character to remove, or -1 if already palindromic/impossible</returns>
    public static int palindromeIndex(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            if (s[left] != s[right])
            {
                // Try removing left character first
                if (isPalindrome(s, left)) return left;

                // Try removing right character
                if (isPalindrome(s, right)) return right;

                // Neither removal works
                return -1;
            }

            left++;
            right--;
        }

        // String is already palindromic
        return -1;
    }

    /// <summary>
    /// Helper function to check if string is palindromic when skipping one character.
    /// Uses two-pointer technique with index skipping logic.
    /// 
    /// Time: O(n), Space: O(1)
    /// </summary>
    /// <param name="s">String to validate</param>
    /// <param name="skipIndex">Index of character to skip during validation</param>
    /// <returns>True if string becomes palindromic when skipIndex is removed</returns>
    public static bool isPalindrome(string s, int skipIndex)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            // Skip the character at skipIndex
            if (left == skipIndex) left++;
            if (right == skipIndex) right--;

            // Check if pointers have crossed after skipping
            if (left >= right) break;

            // If characters don't match, not a palindrome
            if (s[left] != s[right]) return false;

            // Move both pointers inward
            left++;
            right--;
        }

        return true;
    }


    [Theory]
    [InlineData("aa", -1)]                      // Already palindromic - 2 chars
    [InlineData("baa", 0)]                      // Remove first char: 'b' → "aa"
    [InlineData("aab", 2)]                      // Remove last char: 'b' → "aa"
    [InlineData("aba", -1)]                     // Already palindromic - 3 chars
    [InlineData("abc", -1)]                     // No single removal works
    [InlineData("raceacar", 3)]                 // Remove 'e' at index 3 → "racacar" → "racacar" 
    [InlineData("abcba", -1)]                   // Already palindromic - 5 chars
    [InlineData("abcdba", 2)]                   // Remove 'c' at index 2 → "abdba" (palindrome)
    [InlineData("a", -1)]                       // Single character - already palindromic
    [InlineData("madamximadam", 5)]             // Remove 'x' at index 5 → "madamimadam"
    [InlineData("quyjjdcgsvvsgcdjjyq", 1)]      // Remove 'u' at index 1
    public void PalindromeIndex_TestCases(string s, int expected)
    {
        Assert.Equal(expected, palindromeIndex(s));
    }

    [Fact]
    public void PalindromeIndex_EdgeCases_Test()
    {
        // Test empty string (edge case)
        Assert.Equal(-1, palindromeIndex(""));

        // Test very long palindromic string
        string longPalindrome = "abcdefghijklmnopqrstuvwxyzzyxwvutsrqponmlkjihgfedcba";
        Assert.Equal(-1, palindromeIndex(longPalindrome));

        // Test string where removal creates palindrome at boundary
        Assert.Equal(0, palindromeIndex("xaabaa"));  // Remove first 'x'
        Assert.Equal(5, palindromeIndex("aabaax"));  // Remove last 'x'
    }

    [Fact]
    public void IsPalindrome_HelperFunction_Test()
    {
        // Test helper function directly
        Assert.True(isPalindrome("baa", 0));   // Skip 'b' → "aa"
        Assert.True(isPalindrome("aab", 2));   // Skip 'b' → "aa"
        Assert.False(isPalindrome("abc", 1));  // Skip 'b' → "ac" (not palindrome)

        // Test edge cases for helper
        Assert.True(isPalindrome("ab", 0));    // Skip 'a' → "b" (single char)
        Assert.True(isPalindrome("ab", 1));    // Skip 'b' → "a" (single char)
    }
}
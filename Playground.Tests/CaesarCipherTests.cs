using System.Text;

public class CaesarCipherTests
{
    /// <summary>
    /// Encrypts the input string using the Caesar cipher.
    /// Each letter is shifted by k positions in the alphabet, wrapping around as needed.
    /// Non-alphabetic characters remain unchanged.
    /// </summary>
    /// <param name="s">The input string to encrypt.</param>
    /// <param name="k">The number of positions to shift each letter.</param>
    /// <returns>The encrypted string.</returns>
    public string caesarCipher(string s, int k)
    {
        // There are 26 letters in the English alphabet
        const int alphabetLength = 26;
        var sb = new StringBuilder();

        foreach (char c in s)
        {
            // Check if the character is a lowercase letter
            if (c >= 'a' && c <= 'z')
            {
                // Shift within 'a' to 'z' using modular arithmetic
                int shifted = (c - 'a' + k) % alphabetLength + 'a';
                sb.Append((char)shifted);
            }
            // Check if the character is an uppercase letter
            else if (c >= 'A' && c <= 'Z')
            {
                // Shift within 'A' to 'Z' using modular arithmetic
                int shifted = (c - 'A' + k) % alphabetLength + 'A';
                sb.Append((char)shifted);
            }
            else
            {
                // Non-alphabetic characters are unchanged
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    [Theory]
    [InlineData("abc", 3, "def")]
    [InlineData("xyz", 3, "abc")]
    [InlineData("ABC", 3, "DEF")]
    [InlineData("XYZ", 3, "ABC")]
    [InlineData("abcDEF", 3, "defGHI")]
    [InlineData("abc-DEF xyZ", 3, "def-GHI abC")]
    [InlineData("There's-a-starman-waiting-in-the-sky", 3, "Wkhuh'v-d-vwdupdq-zdlwlqj-lq-wkh-vnb")]
    [InlineData("abc", 52, "abc")]
    public void CipherTest(string s, int k, string encrypted)
    {
        Assert.Equal(encrypted, caesarCipher(s, k));
    }

    [Fact]
    public void AsciiTest()
    {
        Assert.Equal(97, 'a');
        Assert.Equal(122, 'z');
        Assert.Equal(65, 'A');
        Assert.Equal(90, 'Z');
    }
}
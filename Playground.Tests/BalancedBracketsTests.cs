public class BalancedBracketsTests
{
    // Method to check if the given string of brackets is balanced
    public string isBalanced(string s)
    {
        // Initialize a stack to keep track of opening brackets
        var stack = new Stack<char>();

        // Iterate through each character in the string
        foreach (char c in s)
        {
            // If the character is an opening bracket, push it onto the stack
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else
            {
                // If the stack is empty, it means there is no matching opening bracket
                if (stack.Count == 0) return "NO";

                // Pop the last opening bracket from the stack
                var open = stack.Pop();

                // Check if the current closing bracket matches the last opening bracket
                if (c == ')' && open != '('
                || c == ']' && open != '['
                || c == '}' && open != '{')
                    return "NO";
            }
        }

        // If the stack is empty, all brackets are balanced; otherwise, they are not
        return stack.Count == 0 ? "YES" : "NO";
    }

    [Theory]
    // Test cases to validate the isBalanced method
    [InlineData("()", "YES")] // Simple balanced brackets
    [InlineData("({[()]})", "YES")] // Nested balanced brackets
    [InlineData("({[()])", "NO")] // Unbalanced due to mismatched closing bracket
    [InlineData("{[(])}", "NO")] // Unbalanced due to incorrect nesting
    [InlineData("", "YES")] // Edge case: empty string is considered balanced
    [InlineData("(((((((((())))))))))", "YES")] // Deeply nested balanced brackets
    [InlineData("(((((((((()))))))))))", "NO")] // Deeply nested unbalanced brackets
    [InlineData("[({})]", "YES")] // Mixed types of balanced brackets
    [InlineData("[({)}]", "NO")] // Mixed types of unbalanced brackets
    public void IsBalanced_TestCases(string s, string expected)
    {
        // Assert that the output matches the expected result
        Assert.Equal(expected, isBalanced(s));
    }
}
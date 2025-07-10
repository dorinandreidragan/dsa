---
name: palindrome index
data_structure: string
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(1)
description: Find the index of character to remove to make string palindromic using two-pointer technique.
---

# palindrome index: two pointers beat brute force

Find which character to remove to make a string palindromic. Brute force tries every character. Smart approach uses two pointers and only checks when needed.

## example

Input: "baa"  
Brute force: Try removing each → "aa", "ba", "ba" → first works → return 0  
Two pointers: Start ends → 'b' ≠ 'a' → try skip left → "aa" palindrome → return 0

Same answer. Fraction of the work.

## how it works

### brute force approach

- For each character position, remove it and check if result is palindrome
- O(n²) time - expensive for large strings
- Simple but inefficient

### two-pointer insight

Start from both ends. If characters match, move inward. When they don't match, exactly one character needs removal.

Try both options:

1. Skip left character, validate remaining substring
2. Skip right character, validate remaining substring

## code

### main function

```csharp
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
```

### validation helper

```csharp
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
```

## why two-pointer wins

**Time complexity:**

- Brute force: O(n²) - check every position
- Two-pointer: O(n) - single pass with minimal validation

**Space complexity:**

- Both approaches: O(1) - no extra storage needed
- Two-pointer has cleaner logic flow

**Reliability:**

- Brute force: Simple but slow
- Two-pointer: Fast and handles edge cases elegantly

The two-pointer approach leverages the palindrome property: if removal works, the mismatch point tells us exactly which character to remove.

### edge cases

- Empty strings and single characters
- Already palindromic strings return -1
- No valid removal possible returns -1
- Proper bounds checking prevents array access errors

### key insight

When two pointers find a mismatch, that's the only place where character removal can help. This eliminates the need to test every position.

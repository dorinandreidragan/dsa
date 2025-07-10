---
name: caesar cipher
data_structure: string
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(n)
description: Shift each letter by k, wrap around, keep non-letters unchanged.
---

# caesar cipher: shift letters, keep the rest

Shift every letter in the input by a fixed number. Wrap around the alphabet. Leave non-letters alone.

## example

Input:  
abc, k = 3 → def  
xyz, k = 3 → abc  
ABC, k = 3 → DEF  
abc-DEF xyZ, k = 3 → def-GHI abC

## how it works

For each character:

- If it's a lowercase letter, shift within 'a' to 'z'.
- If it's uppercase, shift within 'A' to 'Z'.
- Otherwise, keep it as is.

Use modular arithmetic:

- Lowercase: (c - 'a' + k) % 26 + 'a'
- Uppercase: (c - 'A' + k) % 26 + 'A'

## code

```csharp
public string caesarCipher(string s, int k)
{
    const int alphabetLength = 26;
    var sb = new StringBuilder();
    foreach (char c in s)
    {
        if (c >= 'a' && c <= 'z')
            sb.Append((char)((c - 'a' + k) % alphabetLength + 'a'));
        else if (c >= 'A' && c <= 'Z')
            sb.Append((char)((c - 'A' + k) % alphabetLength + 'A'));
        else
            sb.Append(c);
    }
    return sb.ToString();
}
```

### edge cases

- k can be any integer, even > 26.
- Non-letters (spaces, punctuation) are unchanged.
- Case is preserved.

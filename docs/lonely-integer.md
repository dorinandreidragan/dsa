---
name: lonely integer
data_structure: array/list, xor
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(1) (xor)
description: Find the unique element in a list where all others appear twice.
---

# lonely integer: find the one without a pair

Every element appears twice except one. Find the lonely integer.

## example

Input: 1 2 3 2 1
Output: 3

## how it works

- XOR all elements. Pairs cancel out, leaving the unique value.
- Or, count occurrences with a dictionary and return the one with count 1.

O(n) time. O(1) space for XOR, O(n) for dictionary.

## code (XOR method)

```csharp
public int lonelyIntegerWithXor(List<int> a)
{
    int result = 0;
    foreach (int num in a)
    {
        result ^= num;
    }
    return result;
}
```

### edge cases

- Assumes exactly one lonely integer.
- Dictionary method works for any number of duplicates.

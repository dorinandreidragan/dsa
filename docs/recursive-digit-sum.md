---
name: recursive digit sum
data_structure: string
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(1)
description: Calculate digital root using modular arithmetic instead of recursive summation.
---

# recursive digit sum: skip the recursion, use math

Repeatedly sum digits until one remains. The brute force way recurses forever. The smart way uses modular arithmetic and finishes instantly.

## example

Input: n="567", k=3  
Brute force: Concatenate → "567567567" → sum all digits → 54 → 5+4 = 9  
Mathematical: (5+6+7) × 3 = 54 → 54 % 9 = 0 → result is 9

Same answer. Different universe of performance.

## how it works

### brute force approach

- Sum digits of original string
- Multiply by k
- Convert back to string and recursively sum digits until single digit

### mathematical insight

Digital root follows this pattern: any number has the same remainder mod 9 as its digit sum. Skip all intermediate steps.

Formula: `result = totalSum % 9 || 9` (when totalSum > 0)

## code

### recursive solution

```csharp
public static int superDigit(string n, int k)
{
    return superDigitInternal($"{superDigitInternal(n) * k}");
}

public static int superDigitInternal(string n)
{
    if (n.Length == 1)
        return int.Parse($"{n}");

    var sum = 0;
    foreach (var digit in n)
    {
        sum += int.Parse($"{digit}");
    }

    return superDigitInternal($"{sum}");
}
```

### mathematical solution

```csharp
public static int superDigit_ModularArithmetic(string n, int k)
{
    long totalSum = 0;
    foreach (var digit in n)
    {
        totalSum += digit - '0';
    }

    totalSum *= k;

    return totalSum % 9 == 0 ?
        (totalSum == 0 ? 0 : 9) :
        (int)(totalSum % 9);
}
```

## why mathematical wins

**Time complexity:**

- Recursive: O(log(sum) × d) - multiple passes through digits
- Mathematical: O(d) - single pass through original string

**Space complexity:**

- Recursive: O(log(sum) × d) - call stack and string allocations
- Mathematical: O(1) - no recursion, no extra strings

**Reliability:**

- Recursive: Can hit stack overflow on massive inputs
- Mathematical: Handles any input size

The mathematical approach leverages the digital root property: a number and its digit sum are congruent modulo 9. This eliminates all recursive computation.

### edge cases

- Zero input returns zero
- Single digits return themselves
- Multiples of 9 return 9 (except zero)
- Works for any string length and multiplication factor

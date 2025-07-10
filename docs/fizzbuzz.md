---
name: fizzbuzz
data_structure: none
canonical: true
difficulty: basic
time_complexity: O(1) per value
space_complexity: O(1)
description: Print Fizz, Buzz, or FizzBuzz for multiples of 3, 5, or both.
---

# fizzbuzz: multiples get the word

Print numbers. If divisible by 3, print Fizz. If divisible by 5, print Buzz. If both, print FizzBuzz. Otherwise, print the number.

## example

Input: 3 → Fizz
Input: 5 → Buzz
Input: 15 → FizzBuzz
Input: 7 → 7

## how it works

- If value % 15 == 0, return "FizzBuzz"
- Else if value % 3 == 0, return "Fizz"
- Else if value % 5 == 0, return "Buzz"
- Else, return the number as a string

O(1) time per value. O(1) space.

## code

```csharp
private string fizzBuzz(int value)
{
    if (value % 15 == 0) return "FizzBuzz";
    if (value % 3 == 0) return "Fizz";
    if (value % 5 == 0) return "Buzz";
    return value.ToString();
}
```

### edge cases

- Handles any integer input.
- Negative numbers and zero work as expected.

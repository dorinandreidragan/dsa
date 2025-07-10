---
name: counting sort
data_structure: array/list
canonical: true
difficulty: basic
time_complexity: O(n + k)
space_complexity: O(k)
description: Count frequencies of each value to sort without comparisons.
---

# counting sort: count, don't compare

Sort without comparisons. Build a frequency array. Each index counts how many times a value appears.

## example

Input: 9 8 7 3 5 0 0 0 0 0

Frequencies: 5 0 0 1 0 1 0 1 1 1

## how it works

- Create an array of size n (the range of possible values).
- For each value in the input, increment its index in the frequency array.
- Output the frequency array.

O(n + k) time. O(k) space. No swaps, no comparisons.

## code

```csharp
private static List<int> countingSort(List<int> arr, int n = 100)
{
    if (arr == null) throw new ArgumentNullException(nameof(arr));
    var sorted = new List<int>(new int[n]);
    for (int i = 0; i < arr.Count; i++)
    {
        if (arr[i] >= 0 && arr[i] < n)
        {
            sorted[arr[i]]++;
        }
    }
    return sorted;
}
```

### edge cases

- Input values outside [0, n-1] are ignored.
- Works for any non-negative integer range.

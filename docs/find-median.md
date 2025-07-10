---
name: find median
data_structure: array/list
canonical: true
difficulty: basic
time_complexity: O(n log n)
space_complexity: O(1)
description: Sort and return the middle value of a list.
---

# find median: sort, then pick the middle

Find the median in a list. Sort, then select the center value.

## example

Input: 5 3 1 2 5
Sorted: 1 2 3 5 5
Median: 3

## how it works

- Sort the list.
- Return the value at index n / 2 (integer division).

O(n log n) time for sorting. O(1) extra space if sorting in place.

## code

```csharp
private static int myFindMedian(List<int> arr)
{
    arr.Sort();
    return arr[arr.Count / 2];
}
```

### edge cases

- Odd length: returns the middle value.
- Even length: returns the higher of the two middle values (standard for some platforms).

---
name: diagonal difference
data_structure: 2d array/list
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(1)
description: Find the absolute difference between the sums of a matrix's diagonals.
---

# diagonal difference: sum both ways

Square matrix. Find the absolute difference between the sums of its two diagonals.

## example

Input:
1 2 3
4 5 6
9 8 9

First diagonal: 1 + 5 + 9 = 15
Second diagonal: 3 + 5 + 9 = 17
Difference: |15 - 17| = 2

## how it works

- Loop through the matrix.
- Sum the left-to-right diagonal (i, i).
- Sum the right-to-left diagonal (i, n-1-i).
- Return the absolute difference.

O(n) time. O(1) space. No extra arrays.

## code

```csharp
public int diagonalDifference(List<List<int>> arr)
{
    var firstDiagonalSum = 0;
    var secondDiagonalSum = 0;
    var n = arr.Count;
    for (int i = 0; i < n; i++)
    {
        firstDiagonalSum += arr[i][i];
        secondDiagonalSum += arr[i][n - 1 - i];
    }
    return Math.Abs(firstDiagonalSum - secondDiagonalSum);
}
```

### edge cases

- Works for any square matrix.
- Handles negative numbers.

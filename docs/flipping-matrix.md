---
name: flipping matrix
data_structure: 2d array/list
canonical: true
difficulty: intermediate
time_complexity: O(n²)
space_complexity: O(1)
description: Flip rows and columns to maximize the sum of the top-left submatrix.
---

# flipping matrix: maximize the top-left

Given a 2n×2n matrix, flip rows and columns to maximize the sum of the top-left n×n submatrix.

## example

Input (4×4):
112 42 83 119
56 125 56 49
15 78 101 43
62 98 114 108

After optimal flips, top-left 2×2 sum: 125 + 119 + 114 + 101 = 459

## how it works

- For each cell (i, j) in the top-left n×n, pick the max of the 4 possible values that can be moved there by flipping.
- Add up all these maxima.

O(n²) time. O(1) extra space.

## code

```csharp
private static int getMaxSum(List<List<int>> matrix)
{
    int n = matrix.Count / 2;
    int sum = 0;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int val1 = matrix[i][j];
            int val2 = matrix[2 * n - 1 - i][j];
            int val3 = matrix[i][2 * n - 1 - j];
            int val4 = matrix[2 * n - 1 - i][2 * n - 1 - j];
            sum += Math.Max(Math.Max(val1, val2), Math.Max(val3, val4));
        }
    }
    return sum;
}
```

### edge cases

- Matrix must be 2n×2n.
- Handles negative and positive values.

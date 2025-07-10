---
name: grid challenge
data_structure: 2d array/list
canonical: true
difficulty: basic
time_complexity: O(n * m log m)
space_complexity: O(1)
description: Sort each row, then verify all columns are non-decreasing. Works for rectangular grids.
---

# grid challenge: sort rows, check columns

Sort each row of a character grid. Then verify every column reads in non-decreasing order. If both conditions hold, return "YES". Otherwise, "NO". Works for both square and rectangular grids.

## example

Input:
cba
fed
ihg

After sorting rows:
abc
def
ghi

Columns: a-d-g, b-e-h, c-f-i. All ascending. Result: "YES"

Counter-example:
zyx
abc
def

After sorting rows:
xyz
abc
def

First column: x-a-d. Not ascending. Result: "NO"

## how it works

- Sort each row alphabetically using any stable sort.
- Walk through each column from top to bottom.
- If any character is smaller than the one above it, return "NO".
- If all columns pass, return "YES".

O(n × m log m) time for sorting. O(n × m) for column verification. Sorting dominates.

## code

```csharp
public string gridChallenge(List<string> grid)
{
    // Sort each row alphabetically
    for (int i = 0; i < grid.Count; i++)
    {
        var characters = grid[i].ToCharArray();
        Array.Sort(characters);
        grid[i] = new string(characters);
    }

    // Verify all columns are non-decreasing
    int noOfRows = grid.Count;
    int noOfCols = grid[0].Length; // Handle rectangular grids (n×m)
    for (int col = 0; col < noOfCols; col++)
    {
        for (int row = 1; row < noOfRows; row++)
        {
            if (grid[row][col] < grid[row - 1][col])
            {
                return "NO";
            }
        }
    }

    return "YES";
}
```

### edge cases

- Single character grids always work.
- Rectangular grids (n×m) are handled correctly.
- Repeated characters don't break column ordering.
- Early termination saves time on impossible grids.

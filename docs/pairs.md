---
name: pairs
data_structure: hashset, array/list
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(n)
description: Count pairs of integers with a specific difference using HashSet for efficient lookup.
---

# pairs: count elements with target difference

Find pairs of integers where the absolute difference equals a target value k.

## example

Input: k=2, array=[1, 5, 3, 4, 2]  
Output: 3  
Pairs: (1,3), (2,4), (3,5)

## how it works

Use a HashSet for O(1) lookups:

- Store all numbers in a HashSet.
- For each number, check if `number + k` exists in the set.
- Count matches.

This avoids nested loops. The HashSet makes checking existence instant.

## code

```csharp
public int pairs(int k, List<int> arr)
{
    if (arr == null) throw new ArgumentNullException(nameof(arr));
    if (k < 0) throw new ArgumentException("Difference k must be non-negative", nameof(k));

    // Create HashSet for O(1) contains operations
    var numSet = new HashSet<int>(arr);

    // For each number, check if number + k exists in the set
    return arr.Count(num => numSet.Contains(num + k));
}
```

### alternative: two pointers

```csharp
public int pairsTwoPointers(int k, List<int> arr)
{
    if (arr == null) throw new ArgumentNullException(nameof(arr));
    if (k < 0) throw new ArgumentException("Difference k must be non-negative", nameof(k));

    // Sort array to enable two pointers technique
    var sortedArr = arr.OrderBy(x => x).ToList();

    int count = 0;
    int left = 0, right = 1;

    while (right < sortedArr.Count)
    {
        int diff = sortedArr[right] - sortedArr[left];

        if (diff == k)
        {
            count++;
            left++;
            right++;
        }
        else if (diff < k)
        {
            right++;
        }
        else // diff > k
        {
            left++;
            if (left == right)
                right++;
        }
    }

    return count;
}
```

Time: O(n log n) due to sorting, Space: O(1) if sorting in-place. Works by maintaining two pointers and adjusting based on current difference.

### alternative: brute force

```csharp
public int pairsBruteForce(int k, List<int> arr)
{
    int count = 0;
    for (int i = 0; i < arr.Count; i++)
    {
        for (int j = i + 1; j < arr.Count; j++)
        {
            if (Math.Abs(arr[j] - arr[i]) == k)
                count++;
        }
    }
    return count;
}
```

Time: O(n²), Space: O(1). Less efficient but more intuitive.

### edge cases

- Empty array returns 0.
- Single element returns 0.
- Duplicates count correctly - each occurrence can form pairs.
- Order doesn't matter in the array.

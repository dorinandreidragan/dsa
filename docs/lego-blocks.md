---
name: lego blocks
data_structure: dynamic programming
canonical: true
difficulty: advanced
time_complexity: O(w²)
space_complexity: O(w)
description: Count valid wall arrangements without vertical breaks using inclusion-exclusion principle.
---

# lego blocks: when dp meets combinatorics

Build walls with LEGO blocks (widths 1,2,3,4) where no vertical line cuts through all rows. Pure dynamic programming combined with inclusion-exclusion principle creates the solution.

## example

Height=2, Width=3:
Valid arrangements: Each row can be built in 4 ways → Total 4² = 16 → Subtract invalid ones → Result: 9

Invalid pattern: Both rows [1,2] create vertical line after position 1.

## how it works

### three-step algorithm

1. **Single row counting**: Use DP to count ways to fill one row
2. **Total arrangements**: Raise single row count to power of height
3. **Remove invalid patterns**: Apply inclusion-exclusion principle

### single row dp

For width w, count ways using blocks of size 1,2,3,4:

```
ways[0] = 1 (empty row)
ways[i] = ways[i-1] + ways[i-2] + ways[i-3] + ways[i-4]
```

This gives extended Fibonacci sequence: 1,1,2,4,8,15,29...

### inclusion-exclusion magic

To remove invalid arrangements at position j:

- Invalid count = valid[j] × total[width-j]
- Subtract from total arrangements

Work left to right, building valid arrangements incrementally.

## code

```csharp
public static int LegoBlocks(int n, int m)
{
    const long MOD = 1000000007;

    // Step 1: Count ways to fill single row of each width
    var ways = new long[m + 1];
    ways[0] = 1;

    for (int i = 1; i <= m; i++)
    {
        ways[i] = ways[i - 1]; // 1-wide block
        if (i >= 2) ways[i] = (ways[i] + ways[i - 2]) % MOD; // 2-wide block
        if (i >= 3) ways[i] = (ways[i] + ways[i - 3]) % MOD; // 3-wide block
        if (i >= 4) ways[i] = (ways[i] + ways[i - 4]) % MOD; // 4-wide block
    }

    // Step 2: Calculate total arrangements (ways[m]^n)
    var total = new long[m + 1];
    for (int i = 1; i <= m; i++)
    {
        total[i] = (long)BigInteger.ModPow(ways[i], n, MOD);
    }

    // Step 3: Apply inclusion-exclusion to remove invalid patterns
    var valid = new long[m + 1];
    valid[1] = total[1];

    for (int i = 2; i <= m; i++)
    {
        valid[i] = total[i];

        for (int j = 1; j < i; j++)
        {
            valid[i] = (valid[i] - (valid[j] * total[i - j]) % MOD + MOD) % MOD;
        }
    }

    return (int)valid[m];
}
```

## edge cases

- **Height 1**: Skip inclusion-exclusion, return single row count
- **Width 1**: Only one way (all 1-wide blocks)
- **Large values**: Use modular arithmetic throughout to prevent overflow
- **Negative subtraction**: Add MOD before final modulo to handle negative results

## complexity analysis

Time: O(w²) for inclusion-exclusion step dominates  
Space: O(w) for arrays storing intermediate results

The BigInteger.ModPow operation runs in O(log n) but becomes negligible compared to the nested loops.

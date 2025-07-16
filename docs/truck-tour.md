---
name: truck tour
data_structure: array/list
difficulty: intermediate
canonical: true
time_complexity: O(n)
space_complexity: O(1)
description: Find the starting petrol pump index to complete a circular tour.
---

# truck tour: find the starting pump

Determine the starting petrol pump index from which a truck can complete a circular tour. Each pump provides a certain amount of petrol and requires a specific amount to travel to the next pump.

## example

Input:  
Pumps: [(4, 6), (6, 5), (7, 3), (4, 5)]  
Output: 1

Explanation:

- Start at pump 1: Fuel balance = 6 - 5 = 1
- Pump 2: Fuel balance = 1 + (7 - 3) = 5
- Pump 3: Fuel balance = 5 + (4 - 5) = 4
- Pump 0: Fuel balance = 4 + (4 - 6) = 2

The truck completes the circle starting at pump 1.

## how it works

Use a greedy algorithm:

1. Traverse the pumps while maintaining:
   - `fuelBalance`: Current fuel balance.
   - `totalDeficit`: Total fuel deficit when the truck cannot proceed.
2. If `fuelBalance` becomes negative, reset the starting pump to the next index and add the deficit to `totalDeficit`.
3. After the traversal, if `fuelBalance + totalDeficit >= 0`, the tour is possible starting from the last `start` index.

## code

```csharp
public static int truckTour(List<List<int>> petrolPumps)
{
    int start = 0;
    int totalDeficit = 0;
    int fuelBalance = 0;
    for (int i = 0; i < petrolPumps.Count; i++)
    {
        int fuel = petrolPumps[i][0];
        int fuelToNextPump = petrolPumps[i][1];
        fuelBalance += fuel - fuelToNextPump;
        if (fuelBalance < 0)
        {
            totalDeficit += fuelBalance;
            start = i + 1;
            fuelBalance = 0;
        }
    }

    if (totalDeficit + fuelBalance >= 0) return start;

    throw new ArgumentException("No complete tour found!");
}
```

### edge cases

- Single pump with enough fuel.
- Single pump with insufficient fuel.
- Multiple pumps with no valid starting point.
- Large datasets with varying configurations.

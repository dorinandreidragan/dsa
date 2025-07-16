/// <summary>
/// The truck tour problem involves determining the starting petrol pump index from which a truck can complete a circular tour.
/// Each petrol pump provides a certain amount of petrol and requires a specific amount to travel to the next pump.
/// The goal is to find the starting pump index such that the truck can complete the circle without running out of fuel.
/// 
/// Example:
/// Consider the following petrol pumps:
/// Pump 0: Petrol = 4, Distance = 6
/// Pump 1: Petrol = 6, Distance = 5
/// Pump 2: Petrol = 7, Distance = 3
/// Pump 3: Petrol = 4, Distance = 5
/// 
/// Starting at Pump 1, the truck can complete the circle:
/// - From Pump 1 to Pump 2: Fuel balance = 6 - 5 = 1
/// - From Pump 2 to Pump 3: Fuel balance = 1 + (7 - 3) = 5
/// - From Pump 3 to Pump 0: Fuel balance = 5 + (4 - 5) = 4
/// - From Pump 0 to Pump 1: Fuel balance = 4 + (4 - 6) = 2
/// 
/// Therefore, the starting pump index is 1.
/// </summary>
public class TruckTourTests
{
    /// <summary>
    /// Brute-force approach to find the starting petrol pump.
    /// Iterates through all pumps and checks if a complete tour is possible starting from each pump.
    /// Time Complexity: O(N^2), where N is the number of petrol pumps.
    /// Space Complexity: O(1).
    /// </summary>
    public static int truckTour_BruteForce(List<List<int>> petrolPumps)
    {
        var start = 0;
        while (start < petrolPumps.Count)
        {
            if (isCompleteTour(petrolPumps, start)) return start;
            start++;
        }
        throw new ArgumentException("No complete tour found!");
    }

    /// <summary>
    /// Helper method to check if a complete tour is possible starting from a given pump.
    /// Simulates the fuel balance as the truck moves through the pumps.
    /// Returns true if the tour is possible, false otherwise.
    /// </summary>
    public static bool isCompleteTour(List<List<int>> petrolPumps, int start)
    {
        var fuel = 0;
        for (int pumpCounter = 0; pumpCounter < petrolPumps.Count; pumpCounter++)
        {
            int currentPump = (start + pumpCounter) % petrolPumps.Count;
            int pumpFuel = petrolPumps[currentPump][0];
            int fuelToNextPump = petrolPumps[currentPump][1];

            fuel += pumpFuel - fuelToNextPump;
            if (fuel < 0) return false;
        }

        return true;
    }

    /// <summary>
    /// Optimized greedy approach to find the starting petrol pump.
    /// Traverses the pumps once, maintaining a running fuel balance and total deficit.
    /// If the fuel balance becomes negative, resets the starting pump to the next index.
    /// Time Complexity: O(N), where N is the number of petrol pumps.
    /// Space Complexity: O(1).
    /// </summary>
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

    [Theory]
    [MemberData(nameof(TruckTourData))]
    public void TruckTour_TestCases(List<List<int>> petrolPumps, int expectedStart)
    {
        if (expectedStart == -1)
        {
            // Expect an exception when no valid tour exists.
            Assert.Throws<ArgumentException>(() => truckTour(petrolPumps));
        }
        else
        {
            // Validate the result for valid tours.
            var start = truckTour(petrolPumps);
            Assert.Equal(expectedStart, start);
        }
    }

    public static IEnumerable<object[]> TruckTourData =>
    new List<object[]> // Expanded test cases
    {
        // Basic test case with a valid starting pump.
        new object[]
        {
            new List<List<int>>
            {
                new() {1, 3},
                new() {10, 3},
                new() {3, 4},
            },
            1
        },
        // Edge case: Single pump with enough fuel.
        new object[]
        {
            new List<List<int>>
            {
                new() {5, 3},
            },
            0
        },
        // Edge case: Single pump with insufficient fuel.
        new object[]
        {
            new List<List<int>>
            {
                new() {2, 3},
            },
            -1 // Should throw an exception
        },
        // Case with multiple pumps and no valid starting point.
        new object[]
        {
            new List<List<int>>
            {
                new() {1, 5},
                new() {2, 6},
                new() {3, 7},
            },
            -1 // Should throw an exception
        },
        // Case with multiple pumps and a valid starting point.
        new object[]
        {
            new List<List<int>>
            {
                new() {4, 6},
                new() {6, 5},
                new() {7, 3},
                new() {4, 5},
            },
            1
        }
    };
}
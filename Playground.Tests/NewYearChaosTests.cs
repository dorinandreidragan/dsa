
public class NewYearChaosTests
{
    /// <summary>
    /// 1 2 5 3 8 4 6 7
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    private static string minimumBribes(List<int> q)
    {
        // brute force
        int minimumBribes = 0;
        for (int i = 0; i < q.Count; i++)
        {
            if (q[i] - (i + 1) > 2) return "Too chaotic";
            for (int j = Math.Max(0, q[i] - 2); j < i; j++)
            {
                if (q[i] < q[j]) minimumBribes++;
            }
        }
        return $"{minimumBribes}";
    }

    [Theory]
    [MemberData(nameof(MinimumBribesTestData))]
    public void MinimumBribes_TestCases(List<int> q, string expected)
    {
        Assert.Equal(expected, minimumBribes(q));
    }

    public static IEnumerable<object[]> MinimumBribesTestData =>
    [
        [new List<int> {1,2,3,5,4,6,7,8}, "1"]
    ];
}
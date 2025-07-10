
public class NewYearChaosTests_Copy
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
        for (int i = 0; i < q.Count - 1; i++)
        {
            int currentBribes = 0;
            for (int j = i + 1; j < q.Count; j++)
            {
                if (q[i] > q[j])
                {
                    currentBribes++;
                    if (currentBribes > 2)
                        return "Too chaotic";
                }
            }
            minimumBribes += currentBribes;
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
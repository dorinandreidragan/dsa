public class FindMedianTests
{
    private static int myFindMedian(List<int> arr)
    {
        arr.Sort();
        return arr[arr.Count / 2];
    }

    [Theory]
    [MemberData(nameof(MedianTestData))]
    public void MedianTests(List<int> arr, int median)
    {
        Assert.Equal(myFindMedian(arr), median);
    }

    public static IEnumerable<object[]> MedianTestData =>
        [
            [new List<int> { 5, 3, 1, 2, 5 }, 3]
        ];
}
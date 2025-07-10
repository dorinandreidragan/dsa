public class FizzBuzzTests
{
    private string fizzBuzz(int value)
    {
        if (value % 15 == 0) return "FizzBuzz";
        if (value % 3 == 0) return "Fizz";
        if (value % 5 == 0) return "Buzz";
        return value.ToString();
    }

    [Theory]
    [InlineData(3)]
    [InlineData(9)]
    [InlineData(12)]
    public void Returns_Fizz_When_Multiple_Of_3(int value)
    {
        Assert.Equal("Fizz", fizzBuzz(value));
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    public void Returns_Buzz_When_Multiple_Of_5(int value)
    {
        Assert.Equal("Buzz", fizzBuzz(value));
    }

    [Theory]
    [InlineData(15)]
    [InlineData(30)]
    [InlineData(45)]
    public void Returns_FizzBuzz_When_Multiple_Of_15(int value)
    {
        Assert.Equal("FizzBuzz", fizzBuzz(value));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(23)]
    public void Returns_Number_When_Not_Fizz_Buzz(int value)
    {
        Assert.Equal(value.ToString(), fizzBuzz(value));
    }
}
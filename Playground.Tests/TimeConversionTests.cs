using Xunit;

namespace Playground.Tests
{
    public class TimeConversionTestsTests
    {
        [Theory]
        [InlineData("07:05:45PM", "19:05:45")]
        [InlineData("12:00:00AM", "00:00:00")]
        [InlineData("12:00:00PM", "12:00:00")]
        [InlineData("01:00:00AM", "01:00:00")]
        [InlineData("11:59:59PM", "23:59:59")]
        [InlineData("04:59:59AM", "04:59:59")]
        public void TimeConversion_ReturnsExpectedResult(string input, string expected)
        {
            var result = timeConversion(input);
            Assert.Equal(expected, result);
        }

        private string timeConversion(string input)
        {
            // return DateTime.Parse(input).ToString("HH:mm:ss");
            var meridian = input[^2..];
            var timeParts = input[..^2].Split(':');
            int hour = int.Parse(timeParts[0]);

            if (meridian == "PM" && hour < 12)
            {
                hour += 12;
            }
            else if (meridian == "AM" && hour == 12)
            {
                hour = 0;
            }

            return $"{hour:D2}:{timeParts[1]}:{timeParts[2]}";
        }
    }
}
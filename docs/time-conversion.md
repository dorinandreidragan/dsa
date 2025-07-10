---
name: time conversion
data_structure: string
canonical: true
difficulty: basic
time_complexity: O(1)
space_complexity: O(1)
description: Convert a time string from 12-hour AM/PM to 24-hour format.
---

# time conversion: 12-hour to 24-hour

Convert a time string from 12-hour AM/PM format to 24-hour format.

## example

Input: 07:05:45PM → 19:05:45
Input: 12:00:00AM → 00:00:00
Input: 12:00:00PM → 12:00:00

## how it works

- Extract the AM/PM part.
- Split the time into hours, minutes, seconds.
- If PM and hour < 12, add 12 to hour.
- If AM and hour == 12, set hour to 0.
- Format as HH:mm:ss.

O(1) time. O(1) space.

## code

```csharp
private string timeConversion(string input)
{
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
```

### edge cases

- Handles midnight and noon correctly.
- Preserves leading zeros.

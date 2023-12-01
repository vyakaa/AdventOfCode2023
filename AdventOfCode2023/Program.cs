// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Consider your entire calibration document. What is the sum of all of the calibration values?");

var path = @$"{Environment.CurrentDirectory}\1-trebuchet.txt";
var fileContent = File.ReadAllLines(path, Encoding.UTF8);

// part 1
int count = 0;

foreach (var line in fileContent)
{
    int firstDigit = int.Parse(line.First(_ => char.IsNumber(_)).ToString());
    int lastDigit = int.Parse(line.Last(_ => char.IsNumber(_)).ToString());
    count += int.Parse(firstDigit + "" + lastDigit);
}

Console.WriteLine(value: count);

// part 2
var dictionary = new Dictionary<string, int> {
        { "0"    , 0 },
        { "one"  , 1 },
        { "two"  , 2 },
        { "three", 3 },
        { "four" , 4 },
        { "five" , 5 },
        { "six"  , 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine" , 9 },
        { "1"    , 1 },
        { "2"    , 2 },
        { "3"    , 3 },
        { "4"    , 4 },
        { "5"    , 5 },
        { "6"    , 6 },
        { "7"    , 7 },
        { "8"    , 8 },
        { "9"    , 9 },
    };

bool partTwo = true;
var result = fileContent.Select(s => dictionary[Regex.Matches(s, "[0-9]|one|two|three|four|five|six|seven|eight|nine").FirstOrDefault(x => partTwo || int.TryParse(x.Value, out var _))?.Value ?? "0"] * 10 + dictionary[Regex.Matches(s, "[0-9]|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft).FirstOrDefault(x => partTwo || int.TryParse(x.Value, out var _))?.Value ?? "0"]).Sum();

Console.WriteLine(result);

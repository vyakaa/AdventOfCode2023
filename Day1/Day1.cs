using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Text;

namespace Day1
{
    public class DayOneTests
    {
        [Test]
        public void Part_1()
        {
            var path = @$"{Environment.CurrentDirectory}\1-trebuchet.txt";
            var fileContent = File.ReadAllLines(path, Encoding.UTF8);

            int count = 0;

            foreach (var line in fileContent)
            {
                int firstDigit = int.Parse(line.First(_ => char.IsNumber(_)).ToString());
                int lastDigit = int.Parse(line.Last(_ => char.IsNumber(_)).ToString());
                count += int.Parse(firstDigit + "" + lastDigit);
            }

            Console.WriteLine(count);
        }

        [Test]
        public void Part_2_with_regex_found_on_Reddit()
        {
            var path = @$"{Environment.CurrentDirectory}\1-trebuchet.txt";
            var fileContent = File.ReadAllLines(path, Encoding.UTF8);

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
        }

        [Test]
        public void Part_2_simplified()
        {
            var path = @$"{Environment.CurrentDirectory}\1-trebuchet.txt";
            var fileContent = File.ReadAllLines(path, Encoding.UTF8);

            int count = 0;

            foreach (var line in fileContent)
            {
                var replacedContent = line
                    .Replace("one", "o1e")
                    .Replace("two", "t2o")
                    .Replace("three", "t3e")
                    .Replace("four", "4")
                    .Replace("five", "5e")
                    .Replace("six", "6")
                    .Replace("seven", "7n")
                    .Replace("eight", "e8t")
                    .Replace("nine", "n9e");
                int firstDigit = int.Parse(replacedContent.First(_ => char.IsNumber(_)).ToString());
                int lastDigit = int.Parse(replacedContent.Last(_ => char.IsNumber(_)).ToString());
                count += int.Parse(firstDigit + "" + lastDigit);
            }

            Console.WriteLine(count);
        }
    }
}
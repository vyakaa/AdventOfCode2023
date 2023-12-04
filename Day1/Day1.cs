using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Text;

namespace Day1
{
    public class DayOneTests
    {
        [Test]
        public void Part1_test_input_should_return_sum_of_two_digits()
        {
            var input = @"
1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet
";
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int expectedSum = 142;

            int count = CountSumOfParsedDigits(lines);

            Assert.That(count, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Part1_my_input_should_return_sum_of_two_digits()
        {
            var path = $"1-trebuchet.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            int count = CountSumOfParsedDigits(lines);

            Console.WriteLine(count);
        }

        [Test]
        public void Part2_test_input_should_return_sum_of_digits_simplified()
        {
            var input = @"
two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen
";
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int expectedSum = 281;

            int count = CountSumOfParsedDigits(lines, isPartTwo: true);

            Assert.That(count, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Part2_my_input_with_regex_found_on_Reddit_should_return_sum_of_digits()
        {
            var path = $"1-trebuchet.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);

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
            var result = lines.Select(s => dictionary[Regex.Matches(s, "[0-9]|one|two|three|four|five|six|seven|eight|nine").FirstOrDefault(x => partTwo || int.TryParse(x.Value, out var _))?.Value ?? "0"] * 10 + dictionary[Regex.Matches(s, "[0-9]|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft).FirstOrDefault(x => partTwo || int.TryParse(x.Value, out var _))?.Value ?? "0"]).Sum();

            Console.WriteLine(result);
        }

        [Test]
        public void Part2_my_input_should_return_sum_of_digits_simplified()
        {
            var path = $"1-trebuchet.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            int count = CountSumOfParsedDigits(lines, isPartTwo: true);

            Console.WriteLine(count);
        }

        private static int CountSumOfParsedDigits(string[] lines, bool isPartTwo = false)
        {
            int count = 0;

            foreach (var line in lines)
            {
                var replacedContent = line; 
                if (isPartTwo)
                {
                    replacedContent = line
                        .Replace("one", "o1e")
                        .Replace("two", "t2o")
                        .Replace("three", "t3e")
                        .Replace("four", "4")
                        .Replace("five", "5e")
                        .Replace("six", "6")
                        .Replace("seven", "7n")
                        .Replace("eight", "e8t")
                        .Replace("nine", "n9e");
                }

                int firstDigit = int.Parse(replacedContent.First(_ => char.IsNumber(_)).ToString());
                int lastDigit = int.Parse(replacedContent.Last(_ => char.IsNumber(_)).ToString());
                count += int.Parse(firstDigit + "" + lastDigit);
            }

            return count;
        }
    }
}
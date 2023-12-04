using NUnit.Framework;
using System.Text;
using System.Text.RegularExpressions;

namespace Day3
{
    public class DayThreeTests
    {
        [Test]
        public void Part1_test_input_should_return_correct_adjacent_numbers_sum()
        {
            var input = "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..";
            var lines = input.Replace("\r", "").Split('\n').ToList();
            int expectedSum = 4361;

            int sum = CountAdjacentNumbers(lines);
            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Part1_my_input_should_return_adjacent_numbers_sum()
        {
            var path = $"3-gear-ratios.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            int sum = CountAdjacentNumbers(lines.ToList());

            Console.WriteLine($"Sum of adjacent numbers: {sum}");
        }

        [Test]
        public void Part2_test_input_should_return_correct_multiplication_adjacent_numbers_sum()
        {
            var input = "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..";
            var lines = input.Replace("\r", "").Split('\n').ToList();
            int expectedSum = 467835;

            int sum = CountMultiplicationAdjacentNumbers(lines);
            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Part2_my_input_should_return_multiplication_adjacent_numbers_sum()
        {
            var path = $"3-gear-ratios.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            int sum = CountMultiplicationAdjacentNumbers(lines.ToList());

            Console.WriteLine($"Sum of multiplication adjacent numbers: {sum}");
        }

        private static int CountAdjacentNumbers(List<string> lines)
        {
            int sum = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                Regex regex = new Regex(@"\d+");
                foreach (Match match in regex.Matches(line))
                {
                    var number = int.Parse(match.Value);
                    var startIndex = match.Index;
                    var endIndex = startIndex + match.Value.Length - 1;

                    List<(int rowCoord, int colCoord, int number)> numberCoordinates 
                        = GetNumberCoordinates(lines, i, line, startIndex, endIndex, number);

                    sum += IsNumberAdjacentToSymbol(lines, numberCoordinates) ? number : 0;
                }
            }

            return sum;
        }

        private static int CountMultiplicationAdjacentNumbers(List<string> lines)
        {
            var gearLocations = new List<(int rowCoord, int colCoord, int partNumber)>();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                Regex regex = new Regex(@"\d+");
                foreach (Match match in regex.Matches(line))
                {
                    var number = int.Parse(match.Value);
                    var startIndex = match.Index;
                    var endIndex = startIndex + match.Value.Length - 1;

                    List<(int rowCoord, int colCoord, int number)> numberCoordinates 
                        = GetNumberCoordinates(lines, i, line, startIndex, endIndex, number);
                    
                    var adjacentGearLocations = GetAdjacentGearLocations(lines, numberCoordinates);
                    
                    gearLocations.AddRange(adjacentGearLocations);
                }
            }

            var gearRatioSum = gearLocations
                .GroupBy(_ => new
                {
                    _.rowCoord,
                    _.colCoord
                })
                .Select(_ => new
                {
                    row = _.Key.rowCoord,
                    col = _.Key.colCoord,
                    partNumbers = _.Select(_ => _.partNumber).ToList()
                })
                .Where(_ => _.partNumbers.Count == 2)
                .Select(_ => _.partNumbers[0] * _.partNumbers[1])
                .Sum();

            return gearRatioSum;
        }

        private static List<(int rowCoord, int colCoord, int number)> GetNumberCoordinates
            (List<string> lines, int lineIndex, string line, int startIndex, int endIndex, int number)
        {
            var numberCoordinates = new List<(int rowCoord, int colCoord, int number)>();
            for (int i = lineIndex - 1; i < lineIndex + 2; i++)
            {
                for (int j = startIndex - 1; j <= endIndex + 1; j++)
                {
                    numberCoordinates.Add((i, j, number));
                }
            }
            return numberCoordinates
                .Where(_ => _.rowCoord >= 0 && _.rowCoord < lines.Count 
                       && _.colCoord >= 0 && _.colCoord < line.Length)
                .ToList();
        }

        private static bool IsNumberAdjacentToSymbol
            (List<string> lines, List<(int rowCoord, int colCoord, int number)> numberCoordinates)
        {
            foreach (var numberCoordinate in numberCoordinates)
            {
                var cell = lines[numberCoordinate.rowCoord][numberCoordinate.colCoord];
                if (!char.IsNumber(cell) && cell != '.')
                {
                    return true;
                }
            }

            return false;
        }

        private static List<(int row, int col, int partNumber)> GetAdjacentGearLocations
            (List<string> lines, List<(int rowCoord, int colCoord, int partNumber)> numberCoordinates)
        {
            var gearLocations = new List<(int row, int col, int partNumber)>();

            foreach (var numberCoordinate in numberCoordinates)
            {
                var cell = lines[numberCoordinate.rowCoord][numberCoordinate.colCoord];
                if (cell == '*')
                {
                    gearLocations.Add((numberCoordinate.rowCoord, numberCoordinate.colCoord, numberCoordinate.partNumber));
                }
            }

            return gearLocations;
        }
    }
}
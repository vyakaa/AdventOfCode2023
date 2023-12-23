using NUnit.Framework;
using System.Text;

namespace Day6
{
    public class DaySixTests
    {
        [Test]
        public void Part1_test_input_should_return_correct_multiplication_of_number_of_ways_to_beat_the_record()
        {
            var input = @"
Time:      7  15   30
Distance:  9  40  200
";
            var processedInput = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            var times = processedInput[0].Split(':').LastOrDefault()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var distances = processedInput[1].Split(':').LastOrDefault()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            long expectedResult = 288;
            long result = 0;

            for (int i = 0; i < times?.Count; i++)
            {
                long time = long.Parse(times[i]);
                long distance = long.Parse(distances[i]);

                long countWaysToBeatRecord = GetNumberOfWaysToBeatRecord(time, distance);
                result = result == 0 ? result + countWaysToBeatRecord : result * countWaysToBeatRecord;
            }

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Part1_my_input_should_return_multiplication_of_number_of_ways_to_beat_the_record()
        {
            var path = $"6-wait-for-it.txt";
            var processedInput = File.ReadAllLines(path, Encoding.UTF8).ToList();

            var times = processedInput[0].Split(':').LastOrDefault()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var distances = processedInput[1].Split(':').LastOrDefault()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            long result = 0;

            for (int i = 0; i < times?.Count; i++)
            {
                long time = long.Parse(times[i]);
                long distance = long.Parse(distances[i]);

                long countWaysToBeatRecord = GetNumberOfWaysToBeatRecord(time, distance);
                result = result == 0 ? result + countWaysToBeatRecord : result * countWaysToBeatRecord;
            }

            Console.WriteLine($"Multiplication number of ways to beat the record is: {result}");
        }

        [Test]
        public void Part2_test_input_should_return_correct_multiplication_of_number_of_ways_to_beat_the_record()
        {
            var input = @"
Time:      7  15   30
Distance:  9  40  200
";
            var processedInput = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            long time = long.Parse(processedInput[0].Split(':').LastOrDefault()?.Trim().Replace(" ", ""));
            long distance = long.Parse(processedInput[1].Split(':').LastOrDefault()?.Trim().Replace(" ", ""));

            long expectedResult = 71503;

            long countWaysToBeatRecord = GetNumberOfWaysToBeatRecord(time, distance);

            Assert.That(countWaysToBeatRecord, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Part2_my_input_should_return_multiplication_of_number_of_ways_to_beat_the_record()
        {
            var path = $"6-wait-for-it.txt";
            var processedInput = File.ReadAllLines(path, Encoding.UTF8).ToList();

            long time = long.Parse(processedInput[0].Split(':').LastOrDefault()?.Trim().Replace(" ", ""));
            long distance = long.Parse(processedInput[1].Split(':').LastOrDefault()?.Trim().Replace(" ", ""));

            long countWaysToBeatRecord = GetNumberOfWaysToBeatRecord(time, distance);

            Console.WriteLine($"Multiplication number of ways to beat the record is: {countWaysToBeatRecord}");
        }

        public static long GetNumberOfWaysToBeatRecord(long time, long distance)
        {
            long countWaysToBeatRecord = 0;

            for (var holdTheButtonTime = 0; holdTheButtonTime <= time; holdTheButtonTime++)
            {
                long speed = 1 * holdTheButtonTime;
                long timeLeft = time - holdTheButtonTime;

                countWaysToBeatRecord = speed * timeLeft > distance 
                    ? countWaysToBeatRecord + 1 
                    : countWaysToBeatRecord;
            }

            return countWaysToBeatRecord;
        }
    }
}
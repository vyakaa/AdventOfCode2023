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

            int expectedResult = 288;
            int result = 0;

            for (int i = 0; i < times?.Count; i++)
            {
                int time = int.Parse(times[i]);
                int distance = int.Parse(distances[i]);

                int countWaysToBeatRecord = GetNumberOfWaysToBeatRecord(time, distance);
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

            int result = 0;

            for (int i = 0; i < times?.Count; i++)
            {
                int time = int.Parse(times[i]);
                int distance = int.Parse(distances[i]);

                int countWaysToBeatRecord = GetNumberOfWaysToBeatRecord(time, distance);
                result = result == 0 ? result + countWaysToBeatRecord : result * countWaysToBeatRecord;
            }

            Console.WriteLine($"Multiplication number of ways to beat the record is: {result}");
        }

        public static int GetNumberOfWaysToBeatRecord(int time, int distance)
        {
            int countWaysToBeatRecord = 0;

            for (var holdTheButtonTime = 0; holdTheButtonTime <= time; holdTheButtonTime++)
            {
                int speed = 1 * holdTheButtonTime;
                int timeLeft = time - holdTheButtonTime;

                countWaysToBeatRecord = speed * timeLeft > distance 
                    ? countWaysToBeatRecord + 1 
                    : countWaysToBeatRecord;
            }

            return countWaysToBeatRecord;
        }
    }
}
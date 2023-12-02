using NUnit.Framework;
using System.Text;

namespace Day2
{
    public class DayTwoTests
    {
        [Test]
        public void Part1_test_input_should_return_correct_games_ids_sum()
        {
            (int redToPlay, int greenToPlay, int blueToPlay) toPlay = (12, 13, 14);
            var input = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
            var lines = input.Replace("\r", "").Split('\n');

            int sum = 0;

            foreach (var line in lines)
            {
                var (canGameBePlayed, gameId) = CanGameBePlayed(toPlay, line);
                sum += canGameBePlayed ? gameId : 0;
                Console.WriteLine($"Game {gameId} can be played: {canGameBePlayed.ToString().ToUpper()}");
            }

            Console.WriteLine($"Sum of played games ids: {sum}");
            Assert.That(sum, Is.EqualTo(8));
        }

        [Test]
        public void Part1_my_input_should_return_correct_games_ids_sum()
        {
            (int redToPlay, int greenToPlay, int blueToPlay) toPlay = (12, 13, 14);
            var path = $"2-cube-conundrum.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            int sum = 0;

            foreach (var line in lines)
            {
                var (canGameBePlayed, gameId) = CanGameBePlayed(toPlay, line);
                sum += canGameBePlayed ? gameId : 0;
                Console.WriteLine($"Game {gameId} can be played: {canGameBePlayed.ToString().ToUpper()}");
            }

            Console.WriteLine($"Sum of played games ids: {sum}");
        }

        private static (bool canGameBePlayed, int gameId) CanGameBePlayed((int redToPlay, int greenToPlay, int blueToPlay) toPlay, string line)
        {
            bool canGameBePlayed = true;

            var gameId = int.Parse(line.Split(':').First().Split(' ').Last());
            var sets = line.Split(':').Last().Split(";");

            foreach (var set in sets)
            {
                var colors = set.Split(", ");
                var blueCount = int.Parse(colors.FirstOrDefault(_ => _.Contains("blue"))?.Split(" blue")?.FirstOrDefault() ?? "0");
                var redCount = int.Parse(colors.FirstOrDefault(_ => _.Contains("red"))?.Split(" red")?.FirstOrDefault() ?? "0");
                var greenCount = int.Parse(colors.FirstOrDefault(_ => _.Contains("green"))?.Split(" green")?.FirstOrDefault() ?? "0");

                if (blueCount <= toPlay.blueToPlay
                    && redCount <= toPlay.redToPlay
                    && greenCount <= toPlay.greenToPlay)
                {
                    continue;
                }
                else
                {
                    canGameBePlayed = false;
                    break;
                }
            }

            return (canGameBePlayed, gameId);
        }
    }
}
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
            var input = @"
Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
";
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

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
        public void Part1_my_input_should_return_games_ids_sum()
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

        [Test]
        public void Part2_test_input_should_return_correct_power_per_game()
        {
            var input = @"
Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
";
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var expectedPowerPerGame = new[] { 48, 12, 1560, 630, 36 };
            var index = 0;

            foreach (var line in lines)
            {
                var (redToPlay, greenToPlay, blueToPlay) = CountRequiredSetOfCubesPerGame(line);
                var powerOfGame = GetPowerOfGameSet((redToPlay, greenToPlay, blueToPlay));
                Assert.That(powerOfGame, Is.EqualTo(expectedPowerPerGame[index]));
                index++;
            }
        }

        [Test]
        public void Part2_my_input_should_return_sum_of_game_powers()
        {
            var path = $"2-cube-conundrum.txt";
            var lines = File.ReadAllLines(path, Encoding.UTF8);
            int sum = 0;

            foreach (var line in lines)
            {
                var (redToPlay, greenToPlay, blueToPlay) = CountRequiredSetOfCubesPerGame(line);
                var powerOfGame = GetPowerOfGameSet((redToPlay, greenToPlay, blueToPlay));
                sum += powerOfGame;
            }

            Console.WriteLine($"Sum of powers per game: {sum}");
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

        private static (int redToPlay, int greenToPlay, int blueToPlay) CountRequiredSetOfCubesPerGame(string line)
        {
            int redToPlay = 0, greenToPlay = 0, blueToPlay = 0;

            var sets = line.Split(':').Last().Split(";");

            foreach (var set in sets)
            {
                var colors = set.Split(", ");
                var blueCount = int.Parse(colors.FirstOrDefault(_ => _.Contains("blue"))?.Split(" blue")?.FirstOrDefault() ?? "0");
                var redCount = int.Parse(colors.FirstOrDefault(_ => _.Contains("red"))?.Split(" red")?.FirstOrDefault() ?? "0");
                var greenCount = int.Parse(colors.FirstOrDefault(_ => _.Contains("green"))?.Split(" green")?.FirstOrDefault() ?? "0");

                blueToPlay = blueCount > blueToPlay ? blueCount : blueToPlay;
                redToPlay = redCount > redToPlay ? redCount : redToPlay;
                greenToPlay = greenCount > greenToPlay ? greenCount : greenToPlay;
            }

            return (redToPlay, greenToPlay, blueToPlay);
        }

        private static int GetPowerOfGameSet((int redToPlay, int greenToPlay, int blueToPlay) toPlay)
        {
            return toPlay.redToPlay * toPlay.greenToPlay * toPlay.blueToPlay;
        }
    }
}
using NUnit.Framework;
using System.Text;

namespace Day4
{
    public class DayFourTests
    {
        [Test]
        public void Part1_test_input_should_return_correct_sum_of_winning_cards()
        {
            var input = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
            var cards = input.Replace("\r", "").Split('\n').ToList();
            int expectedSum = 13;

            int sum = CountSumOfWinningCards(cards);
            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Part1_my_input_should_return_sum_of_winning_cards()
        {
            var path = $"4-scratchcards.txt";
            var cards = File.ReadAllLines(path, Encoding.UTF8).ToList();
            int sum = CountSumOfWinningCards(cards);

            Console.WriteLine($"Sum of winning cards is: {sum}");
        }

        private static int CountSumOfWinningCards(List<string> cards)
        {
            int sum = 0;
            foreach (var card in cards)
            {
                var listOfWinningNumbers = card.Substring(card.IndexOf(':') + 1, card.IndexOf('|') - card.IndexOf(':') - 1)
                    .Trim()
                    .Split(' ')
                    .Select(_ => _.Trim())
                    .ToList();
                var myNumbers = card.Split('|')
                    .LastOrDefault()?
                    .Trim()
                    .Split(' ')
                    .Where(_ => _.Trim() != "")
                    .Select(_ => _.Trim())
                    .ToList();

                var cardPrice = 0;
                foreach (var myNumber in myNumbers)
                {
                    if (listOfWinningNumbers.Contains(myNumber))
                        cardPrice = cardPrice > 0 ? cardPrice * 2 : cardPrice + 1;
                }

                sum += cardPrice;
            }

            return sum;
        }
    }
}
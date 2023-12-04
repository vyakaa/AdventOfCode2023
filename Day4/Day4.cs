using NUnit.Framework;
using System.Text;

namespace Day4
{
    public class DayFourTests
    {
        [Test]
        public void Part1_test_input_should_return_correct_sum_of_winning_cards()
        {
            var input = @"
Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
";
            var cards = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            int expectedSum = 13;

            int sum = CountSumOfWinningCards(cards).sum;
            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Part1_my_input_should_return_sum_of_winning_cards()
        {
            var path = $"4-scratchcards.txt";
            var cards = File.ReadAllLines(path, Encoding.UTF8).ToList();
            int sum = CountSumOfWinningCards(cards).sum;

            Console.WriteLine($"Sum of winning cards is: {sum}");
        }

        [Test]
        public void Part2_test_input_should_return_correct_total_of_cards()
        {
            var input = @"
Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
";
            var cards = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            int expectedTotal = 30;

            int total = CountTotalScratchcardsWithList(cards);
            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void Part2_my_input__with_dictionary_should_return_total_scratchcards()
        {
            var path = $"4-scratchcards.txt";
            var cards = File.ReadAllLines(path, Encoding.UTF8).ToList();

            List<int> winsByCards = CountSumOfWinningCards(cards).winsByCards;
            int total = CountTotalScratchcardsWithDictionary(winsByCards);

            Console.WriteLine($"Sum of winning cards is: {total}");
        }

        [Test]
        public void Part2_my_input_with_list_should_return_total_scratchcards()
        {
            var path = $"4-scratchcards.txt";
            var cards = File.ReadAllLines(path, Encoding.UTF8).ToList();
            int total = CountTotalScratchcardsWithList(cards);

            Console.WriteLine($"Sum of winning cards is: {total}");
        }

        private static (List<int> winsByCards, int sum) CountSumOfWinningCards(List<string> cards)
        {
            int sum = 0;
            List<int> winsByCards = new List<int>();
            foreach (var card in cards)
            {
                var listOfWinningNumbers = card.Substring(card.IndexOf(':') + 1, card.IndexOf('|') - card.IndexOf(':') - 1)
                    .Trim()
                    .Split(' ')
                    .Select(_ => _.Trim())
                    .ToHashSet();
                var myNumbers = card.Split('|')
                    .LastOrDefault()?
                    .Trim()
                    .Split(' ')
                    .Where(_ => _.Trim() != "")
                    .Select(_ => _.Trim())
                    .ToHashSet();

                int cardScore = 0;
                int winningNumbersCount = 0;
                foreach (var myNumber in myNumbers)
                {
                    if (listOfWinningNumbers.Contains(myNumber))
                    {
                        cardScore = cardScore > 0 ? cardScore * 2 : cardScore + 1;
                        winningNumbersCount++;
                    }
                }

                sum += cardScore;
                winsByCards.Add(winningNumbersCount);
            }

            return (winsByCards, sum);
        }

        private static int CountTotalScratchcardsWithList(List<string> cards)
        {
            List<int> winsByCards = CountSumOfWinningCards(cards).winsByCards;
            var listOfCopies = new List<int>();
            for (int i = 0; i < winsByCards.Count; i++)
            {
                var winsByCard = winsByCards[i];
                if (winsByCard == 0)
                    continue;
                var copiesOfCurrCard = listOfCopies.Count(x => x == i) + 1;
                for (int j = i + 1; (j - i) <= copiesOfCurrCard; j++)
                {
                    for (int k = 0; k < winsByCard; k++)
                    {
                        listOfCopies.Add(k + i + 1);
                    }
                }
            }

            return listOfCopies.Count() + winsByCards.Count;
        }

        private static int CountTotalScratchcardsWithDictionary(List<int> cardScores)
        {
            int total = cardScores.Count;
            var dict = new Dictionary<int, int>();

            for (int i = 0; i < cardScores.Count; i++)
            {
                var cardCopiesCount = dict.ContainsKey(i) ? dict[i] + 1 : 1;

                for (var cardId = i + 1; (cardId - i) <= cardScores[i]; cardId++)
                {
                    dict[cardId] = dict.TryGetValue(cardId, out var val)
                        ? val + 1 * cardCopiesCount
                        : 1 * cardCopiesCount;
                }
            }

            total += dict.Values.Sum();
            return total;
        }
    }
}
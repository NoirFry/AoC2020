using System;
using System.Collections.Generic;

namespace Day22
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            List<int> deck1 = new();

            List<int> deck2 = new();

            ParseInput(inputLines, deck1, deck2);

            int round = 0;
            do
            {
                P1GameRound(deck1, deck2);
                round++;
            } while (deck1.Count != 0 && deck2.Count != 0);

            int part1 = 0;
            List<int> winner = deck1.Count > 0 ? deck1 : deck2;

            for (int i = 0; i < winner.Count; i++)
            {
                part1 += winner[i] * (winner.Count - i);
            }

            Console.WriteLine($"Part1: {part1} (right answer: 34664)");
            deck1.Clear();
            deck2.Clear();
            ParseInput(inputLines, deck1, deck2);

            round = 0;
            do
            {
                P2GameRound(deck1, deck2);
                round++;
            } while (deck1.Count != 0 && deck2.Count != 0);

            int part2 = 0;
            winner = deck1.Count > 0 ? deck1 : deck2;

            for (int i = 0; i < winner.Count; i++)
            {
                part2 += winner[i] * (winner.Count - i);
            }

            Console.WriteLine($"Part2: {part2} (right answer: 32018)");
            Console.ReadKey();
        }

        private static bool P1GameRound(List<int> deck1, List<int> deck2)
        {
            int top1 = deck1[0];
            int top2 = deck2[0];
            deck1.RemoveAt(0);
            deck2.RemoveAt(0);
            if (top1 > top2)
            {
                deck1.Add(top1);
                deck1.Add(top2);
                return true;
            }
            else
            {
                deck2.Add(top2);
                deck2.Add(top1);
                return false;
            }
        }

        private static bool P2GameRound(List<int> deck1, List<int> deck2)
        {
            int top1 = deck1[0];
            int top2 = deck2[0];

            if (deck1.Count - top1 > 0 && deck2.Count - top2 > 0)
            {
                List<int> subDeck1 = new();
                List<int> subDeck2 = new();
                subDeck1.AddRange(deck1.GetRange(1, top1));
                subDeck2.AddRange(deck2.GetRange(1, top2));
                bool result;
                int round = 0;
                do
                {
                    result = P2GameRound(subDeck1, subDeck2);
                    round++;
                } while (subDeck1.Count != 0 && subDeck2.Count != 0 && round < 1000);

                if (result || round == 1000)
                {
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);
                    deck1.Add(top1);
                    deck1.Add(top2);
                    return true;
                }
                else
                {
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);
                    deck2.Add(top2);
                    deck2.Add(top1);
                    return false;
                }
            }
            else
            {
                return P1GameRound(deck1, deck2);
            }
        }

        private static void ParseInput(string[] inputLines, List<int> deck1, List<int> deck2)
        {
            bool p2Started = false;
            foreach (string line in inputLines[1..])
            {
                if (line == string.Empty)
                    continue;
                if (line == "Player 2:")
                {
                    p2Started = true;
                    continue;
                }
                int card = Convert.ToInt32(line);
                if (p2Started)
                    deck2.Add(card);
                else
                    deck1.Add(card);
            }
        }
    }
}

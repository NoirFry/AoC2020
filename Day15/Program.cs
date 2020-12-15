using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day15
{
    internal class Program
    {
        private static void Main()
        {

            // key - number, value - last step and pre-last step
            Dictionary<int, int> game = new Dictionary<int, int>
            {
                {19, 1 },
                {0, 2 },
                {5, 3 },
                {1, 4 },
                {10, 5 },
            };

            int lastNumber = 13;
            int curNumber = 0;

            for (int step = 7; step < 30000001; step++)
            {
                curNumber = 0;
                if (game.TryGetValue(lastNumber, out int last))
                {
                    curNumber = step - 1 - last;
                    if (curNumber == 0)
                        curNumber++;
                    game[lastNumber] = step - 1;
                }
                else
                    game.Add(lastNumber, step - 1);

                lastNumber = curNumber;

                if (step == 2020)
                    Console.WriteLine($"Part 1: {curNumber} (right answer: 1015)");
            }

            Console.WriteLine($"Part 2: {curNumber} (right answer: 201)");
            Console.ReadKey();
        }
    }
}

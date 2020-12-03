using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    internal class Program
    {
        private const string InputFileName = @"..\..\..\Input.txt";

        private static void Main()
        {
            string[] inputLines = File.ReadAllLines(InputFileName);

            int x = 0;
            int slopeWidth = inputLines[0].Length;
            int offset = 1;
            bool lastRide = false;
            bool skip = false;

            List<int> treeCount = new List<int>
            {
                0,
                0,
                0,
                0,
                0,
            };

            for (int ride = 0; ride < 5; ride++)
            {
                foreach (string line in inputLines)
                {
                    if (lastRide)
                    {
                        if (skip)
                        {
                            skip = false;
                            continue;
                        }
                        else
                            skip = true;
                    }

                    if (line[x] == '#')
                        treeCount[ride]++;

                    x += offset;
                    if (x >= slopeWidth)
                        x -= slopeWidth;
                }
                offset += 2;
                x = 0;
                if (offset == 9)
                {
                    offset = 1;
                    lastRide = true;
                }
            }

            long totalTreesAllRides = 1;
            foreach (int i in treeCount)
            {
                    totalTreesAllRides *= i;
            }

            Console.WriteLine($"Part1: total trees: {treeCount[1]}");
            Console.WriteLine($"Part2: total trees in all paths: {totalTreesAllRides}");

            Console.ReadKey();
        }
    }
}

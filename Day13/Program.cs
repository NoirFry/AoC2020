using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day13
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = File.ReadAllLines(InputFileName);

            int time = Convert.ToInt32(inputLines[0], CultureInfo.InvariantCulture);

            List<int> buses = new();

            foreach (string str in inputLines[1].Split(',', StringSplitOptions.None))
            {
                if (str == "x")
                    continue;
                buses.Add(Convert.ToInt32(str, CultureInfo.InvariantCulture));
            }

            int minbus = 0;
            int mintime = int.MaxValue;

            foreach (int bus in buses)
            {
                int m = time % bus;
                m = bus - m;
                if (m < mintime)
                {
                    mintime = m;
                    minbus = bus;
                }
            }

            Console.WriteLine($"Part1: {minbus * mintime} (right answer: 222)");

            // Part 2

            // store bus number and its required offset in minutes together
            List<(int bus, int offset)> busesV2 = new();

            int offsetCount = -1;
            foreach (string str in inputLines[1].Split(',', StringSplitOptions.None))
            {
                offsetCount++;

                if (str == "x")
                    continue;
                (int bus, int offset) t = (Convert.ToInt32(str, CultureInfo.InvariantCulture), offsetCount);
                busesV2.Add(t);
            }

            long solvedTime = busesV2[0].bus;
            long period = busesV2[0].bus;

            for (int i = 1; i < busesV2.Count; i++)
            {
                long nextBus = busesV2[i].bus;
                long nextOffset = busesV2[i].offset;

                solvedTime = MinTimeIntersectionWithOffset(solvedTime, period, nextBus, nextOffset);

                period *= busesV2[i].bus;
            }

            Console.WriteLine($"Part2: {solvedTime} (right answer: 408270049879073)");
            Console.ReadKey();
        }

        private static long MinTimeIntersectionWithOffset(long solvedTime, long period, long nextBus, long offset)
        {
            long counter = 0;
            do
            {
                counter++;
                long tryTime = solvedTime + period * counter;
                if ((tryTime + offset) % nextBus == 0)
                {
                    return solvedTime + counter * period;
                }
            } while (true);
        }
    }
}

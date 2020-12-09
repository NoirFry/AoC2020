namespace Day09
{
    using System;
    using System.Globalization;
    using System.IO;

    internal class Program
    {
        private const int windowSize = 25;

        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = File.ReadAllLines(InputFileName);

            long[] window = new long[windowSize];

            for (int i = 0; i < windowSize; i++)
            {
                window[i] = ToLong(inputLines[i]);
            }

            int position = 0;
            long part1 = 0;

            for (int i = windowSize; i < inputLines.Length; i++)
            {
                long curNum = ToLong(inputLines[i]);

                if (HasSum(window, curNum))
                {
                    window[position] = curNum;
                }
                else
                {
                    part1 = curNum;
                    Console.WriteLine($"Part1: {curNum}, (right answer: 25918798)");
                    break;
                }
                position = position == windowSize - 1 ? 0 : position + 1;
            }

            long[] inputNumbers = new long[inputLines.Length];
            for (int i = 0; i < inputLines.Length; i++)
            {
                inputNumbers[i] = ToLong(inputLines[i]);
            }


            for (int i = 0; i < inputNumbers.Length; i++)
            {
                long cur = GetSumFromPos(inputNumbers, i, part1);
                if (cur != -1)
                {
                    Console.WriteLine($"Part2: {cur}, (right answer: 3340942)");
                    break;
                }
            }

            Console.ReadKey();
        }

        private static long ToLong(string str)
        {
            return Convert.ToInt64(str, CultureInfo.InvariantCulture);
        }

        private static bool HasSum(long[] windows, long num)
        {
            for (int i = 1; i < windowSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (windows[i] + windows[j] == num)
                        return true;
                }
            }
            return false;
        }

        private static long GetSumFromPos(long[] input, int startPos, long target)
        {
            long sum = 0;
            long min = input[startPos];
            long max = input[startPos];
            int ind = startPos;

            do
            {
                sum += input[ind];
                if (sum == target)
                {
                    return min + max;
                }
                ind++;
                if (min > input[ind])
                    min = input[ind];
                if (max < input[ind])
                    max = input[ind];
            } while (sum < target);
            return -1;
        }
    }
}


namespace Day10
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = File.ReadAllLines(InputFileName);

            List<int> inputNums = inputLines.Select(line => ToInt(line)).ToList();
            inputNums.Sort();
            inputNums.Add(inputNums[^1] + 3);

            int prevNum = 0;
            int diff1 = 0;
            int diff3 = 0;
            int[] groups = new int[100];
            int groupIndex = 0;
            groups[0]++;

            foreach (int curNum in inputNums)
            {
                if (curNum - prevNum == 1)
                {
                    diff1++;
                    groups[groupIndex]++;
                }
                else if (curNum - prevNum == 3)
                {
                    diff3++;
                    groupIndex++;
                    groups[groupIndex]++;
                }

                prevNum = curNum;
            }

            long mult = 1;
            foreach(int group in groups)
            {
                if (group == 3)
                    mult *= 2;
                else if (group == 4)
                    mult *= 4;
                else if (group == 5)
                    mult *= 7;
            }

            Console.WriteLine($"Part1: diff1 * diff3: {diff1 * diff3} (right answer: 2210)");
            Console.WriteLine($"Part2: {mult} (right answer: 7086739046912)");
            Console.ReadKey();
        }

        private static int ToInt(string str)
        {
            return Convert.ToInt32(str, CultureInfo.InvariantCulture);
        }
    }
}

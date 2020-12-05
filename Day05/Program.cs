using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    internal class Program
    {
        private const string InputFileName = @"..\..\..\Input.txt";

        private static void Main()
        {
            string inputText = File.ReadAllText(InputFileName);

            string binaryText = inputText.Replace('B', '1')
                                         .Replace('F', '0')
                                         .Replace('L', '0')
                                         .Replace('R', '1');

            string[] binaryLines = binaryText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            List<int> seats = new();

            foreach (string line in binaryLines)
            {
                seats.Add(Convert.ToInt32(line, 2));
            }
            seats.Sort();

            int prevSeadId = seats[0] - 1;
            int answerPart2 = 0;
            foreach (int seatId in seats)
            {
                if (prevSeadId + 1 != seatId)
                    answerPart2 = prevSeadId + 1;
                prevSeadId = seatId;
            }


            Console.WriteLine($"Part1: highest seat ID: {seats.Max()}");
            Console.WriteLine($"Part2: your seat ID: {answerPart2}");
            Console.ReadKey();
        }
    }
}

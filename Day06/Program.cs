using System;
using System.IO;

namespace Day06
{
    internal class Program
    {
        private const string InputFileName = @"..\..\..\Input.txt";

        private static void Main()
        {
            string inputText = File.ReadAllText(InputFileName);

            string[] groups = inputText.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < groups.Length; i++)
            {
                groups[i] = groups[i].Replace("\r\n", "");
            }

            int sumPart1 = 0;

            for (int i = 0; i < groups.Length; i++)
            {
                bool[] answerMap = new bool[26];
                for (int j = 0; j < groups[i].Length; j++)
                {
                    if (answerMap[(int)groups[i][j] - (int)'a'])
                        continue;
                    answerMap[(int)groups[i][j] - (int)'a'] = true;
                    sumPart1++;
                }
            }

            Console.WriteLine($"Part1: sum: {sumPart1}");
            groups = inputText.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            int sumPart2 = 0;

            foreach (string group in groups)
            {
                string[] members = group.Split(new[] { "\r\n" }, StringSplitOptions.None);
                int[] answerMap = new int[26];

                foreach (string member in members)
                {
                    for (int charPos = 0; charPos < member.Length; charPos++)
                    {
                        answerMap[member[charPos] - 'a']++;
                    }
                }

                foreach (int answer in answerMap)
                {
                    if (answer == members.Length)
                        sumPart2++;
                }
            }

            Console.WriteLine($"Part2: sum: {sumPart2}");
            Console.ReadKey();
        }
    }
}

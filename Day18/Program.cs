using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            long part1 = 0;

            foreach (string line in inputLines)
            {
                string trimLine = line.Replace(" ", null, StringComparison.Ordinal);
                string decoLine = "(" + trimLine + ")";
                long lineEval = 0;
                int linePosition = 1;
                lineEval = Evaluate(decoLine, ref linePosition);
                part1 += lineEval;
                //Console.WriteLine($"line result: {lineEval}");
            }

            Console.WriteLine($"Part1: {part1} (right answer: 6811433855019)");

            // Part 2

            long part2 = 0;
            foreach (string line in inputLines)
            {
                string trimLine = line.Replace(" ", null, StringComparison.Ordinal);
                string orderedLine = DecoLine(trimLine, out _);
                string decoLine = "(" + orderedLine + ")";
                long lineEval = 0;
                int linePosition = 1;
                lineEval = Evaluate(decoLine, ref linePosition);
                part2 += lineEval;
                //Console.WriteLine($"line result: {lineEval}");
            }

            Console.WriteLine($"Part1: {part2} (right answer: 129770152447927)");
            Console.ReadKey();
        }

        private static long Evaluate(string line, ref int linePosition)
        {
            bool sum = true;
            long curNumber;
            long lineEval = 0;

            do
            {
                if (line[linePosition] == '(')
                {
                    linePosition++;
                    curNumber = Evaluate(line, ref linePosition);
                    if (sum)
                        lineEval += curNumber;
                    else
                        lineEval *= curNumber;
                }
                else if (line[linePosition] == '+')
                    sum = true;
                else if (line[linePosition] == '*')
                    sum = false;
                else
                {
                    curNumber = Convert.ToInt32(line[linePosition].ToString());
                    if (sum)
                        lineEval += curNumber;
                    else
                        lineEval *= curNumber;
                }

                linePosition++;

            } while (line[linePosition] != ')');

            //linePosition++;

            return lineEval;
        }

        private static string DecoLine(string line, out int counter)
        {
            List<char>[] groups = new List<char>[20];
            int curGroup = 0;
            groups[curGroup] = new List<char>();
            int t = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '(')
                {
                    string ret = DecoLine(line[(i + 1)..], out int c);
                    groups[curGroup].AddRange(ret.ToCharArray());
                    i += c+1;
                }
                else if (line[i] == ')')
                {
                    t = i;
                    break;
                }
                else if (line[i] == '*')
                {
                    curGroup++;
                    groups[curGroup] = new List<char>();
                }
                else
                {
                    groups[curGroup].Add(line[i]);
                }
            }
            counter = t;
            return "(" + string.Join('*', groups.Where(x => x != null).Select(x => "(" + new string(x.ToArray()) + ")")) + ")";
        }
    }
}

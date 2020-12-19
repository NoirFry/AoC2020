using System;
using System.Collections.Generic;

namespace Day19
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);
            List<string> inputRules = new();
            List<string> inputStrings = new();

            //Rule r4 = new Rule('a');
            //Rule r5 = new Rule('b');
            //Rule r3 = new Rule(r4, r5, r5, r4);
            //Rule r2 = new Rule(r4, r4, r5, r5);
            //Rule r1 = new Rule(r2, r3, r3, r2);
            //Rule r0 = new Rule(r1, r5);

            //string test = "babbb";
            //Console.WriteLine($"test: {test}, result: {r0.IsMatch(test, out int count) == true && count == test.Length}");

            // fill rules
            Rule[] rules = new Rule[140];

            bool processRules = true;

            foreach (string line in inputLines)
            {
                if (line == "")
                {
                    processRules = false;
                    continue;
                }

                if (processRules)
                    inputRules.Add(line);
                else
                    inputStrings.Add(line);
            }

            Rule r0 = Rule.Resolve(0, inputRules, rules);

            int matched = 0;

            foreach (string line in inputStrings)
            {
                if (r0.IsMatch(line, out int count) == true && count >= line.Length)
                {
                    matched++;
                }
            }

            Console.WriteLine($"Part1: {matched} (right answer: 162)");

            Rule[] rules2 = new Rule[140];

            r0 = Rule.Resolve(0, inputRules, rules2, true);

            matched = 0;

            foreach (string line in inputStrings)
            {
                if (r0.IsMatch(line, out int count) == true && count >= line.Length)
                {
                    matched++;
                }
            }

            Console.WriteLine($"Part2: {matched} (right answer: 267)");
            Console.ReadKey();
        }
    }
}

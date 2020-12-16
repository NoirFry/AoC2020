using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            // parse rules
            List<Rule> rules = new();
            List<int> myTicket = new();

            bool ticketsStarted = false;
            bool yourTicketStarted = false;
            bool myTicketDone = false;
            int invalidFields = 0;

            foreach (string line in inputLines)
            {
                if (line == "")
                    continue;

                if (line == "your ticket:")
                {
                    yourTicketStarted = true;
                    continue;
                }

                if (yourTicketStarted && !myTicketDone)
                {
                    myTicket = line.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                    myTicketDone = true;
                }

                if (line == "nearby tickets:")
                {
                    ticketsStarted = true;
                    continue;
                }

                if (!yourTicketStarted && !ticketsStarted)
                {
                    string name = line.Split(':')[0];
                    string[] ruleFields = line.Split(": ")[1].Split(new[] { " or ", "-" }, StringSplitOptions.None);
                    rules.Add(new Rule(name, Convert.ToInt32(ruleFields[0]), Convert.ToInt32(ruleFields[1]), Convert.ToInt32(ruleFields[2]), Convert.ToInt32(ruleFields[3])));
                }

                if (ticketsStarted)
                {
                    List<int> ticketValues = line.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                    for (int i = 0; i < ticketValues.Count; i++)
                    {
                        int val = ticketValues[i];
                        bool valid = false;
                        int validCount = 0;
                        int invalidCount = 0;
                        int invalidRule = -1;
                        for (int ruleIndex = 0; ruleIndex < rules.Count; ruleIndex++)
                        {
                            Rule rule = rules[ruleIndex];
                            if (rule.IsValid(val))
                            {
                                valid = true;
                                validCount++;
                            }
                            else
                            {
                                invalidRule = ruleIndex;
                                invalidCount++;
                            }
                        }
                        if (!valid)
                        {
                            invalidFields += val;
                            continue;
                        }

                        if (invalidCount == 1)
                        {
                            rules[invalidRule].AllowedColumns.Remove(i);
                        }
                    }
                }
            }

            Console.WriteLine($"Part1: {invalidFields} (right answer: 18142)");

            foreach (Rule rule in rules.OrderBy(x => x.AllowedColumns.Count).ToList())
            {
                int remval = rule.AllowedColumns[0];
                foreach (Rule r in rules)
                {
                    if (r.AllowedColumns.Count >= 2)
                        r.AllowedColumns.Remove(remval);
                }
            }

            long part2 = 1;
            foreach (Rule r in rules)
            {
                if (r.IsDeparture)
                {
                    part2 *= myTicket[r.AllowedColumns[0]];
                }
            }

            Console.WriteLine($"Part2: {part2} (right answer: 1069784384303)");
            Console.ReadKey();
        }
    }
}

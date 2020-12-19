using System;
using System.Collections.Generic;

namespace Day19
{
    internal class Rule
    {
        private readonly int number = -1;

        private readonly char? c = null;

        private readonly Rule?[] subRules = new Rule[5];

        private bool IsFinal => c != null;

        private bool HasOr => subRules[2] != null;

        public Rule(char c)
        {
            this.c = c;
        }

        public Rule(int n, Rule r1, Rule? r2, Rule? r3, Rule? r4)
        {
            number = n;
            subRules[0] = r1;
            subRules[1] = r2;
            subRules[2] = r3;
            subRules[3] = r4;

        }

        public bool IsMatch(string input, out int count)
        {
            if (input.Length == 0)
            {
                count = 0;
                return true;
            }

            if (IsFinal)
            {
                if (input[0] == c)
                {
                    count = 1;
                    return true;
                }
                count = -1;
                return false;
            }

            if (subRules[0]!.IsMatch(input, out int firstCount))
            {
                if (subRules[1] == null)
                {
                    count = firstCount;
                    return true;
                }
                else if (subRules[1]!.IsMatch(input[firstCount..], out int secondCount))
                {
                    count = firstCount + secondCount;
                    return true;
                }
            }

            if (HasOr)
            {
                if (subRules[2]!.IsMatch(input, out int thirdCount))
                {
                    if (subRules[3] != null)
                    {
                        if (subRules[3]!.IsMatch(input[thirdCount..], out int forthCount))
                        {
                            if (subRules[4] != null)
                            {
                                if (subRules[4]!.IsMatch(input[forthCount..], out int fifthCount))
                                {
                                    count = thirdCount + forthCount + fifthCount;
                                    return true;
                                }
                            }
                            else
                            {
                                count = thirdCount + forthCount;
                                return true;
                            }
                        }
                    }
                    else
                    {
                        count = thirdCount;
                        return true;
                    }
                }
            }

            count = -1;
            return false;
        }

        public static Rule Resolve(int ruleNumber, List<string> input, Rule[] rules, bool isPart2 = false)
        {
            if (rules[ruleNumber] != null)
                return rules[ruleNumber];

            if (isPart2)
            {
                if (ruleNumber == 8)
                {
                    Rule r42 = Resolve(42, input, rules, true);
                    Rule r = new Rule(8, r42, null, r42, null);
                    r.subRules[3] = r;
                    rules[ruleNumber] = r;
                    return r;
                }
                if (ruleNumber == 11)
                {
                    Rule r42 = Resolve(42, input, rules, true);
                    Rule r31 = Resolve(31, input, rules, true);
                    Rule r = new Rule(11, r42, r31, r42, null);
                    r.subRules[3] = r;
                    r.subRules[4] = r31;
                    rules[ruleNumber] = r;
                    return r;
                }
            }

            Rule? result = null;

            foreach (string line in input)
            {
                string strNum = line.Split(": ")[0];
                if (Convert.ToInt32(strNum) == ruleNumber)
                {
                    string strRules = line.Split(": ")[1];

                    if (strRules == "\"a\"")
                    {
                        rules[ruleNumber] = new Rule('a');
                        return rules[ruleNumber];
                    }
                    if (strRules == "\"b\"")
                    {
                        rules[ruleNumber] = new Rule('b');
                        return rules[ruleNumber];
                    }

                    string[] parts = strRules.Split(" | ");
                    string[] part1numbers = parts[0].Split(' ');

                    int r1n = Convert.ToInt32(part1numbers[0]);
                    Rule r1;
                    Rule? r2 = null, r3 = null, r4 = null;

                    if (rules[r1n] == null)
                        r1 = Resolve(r1n, input, rules, isPart2);
                    r1 = rules[r1n];

                    if (part1numbers.Length == 2)
                    {
                        int r2n = Convert.ToInt32(part1numbers[1]);
                        if (rules[r2n] == null)
                            r2 = Resolve(r2n, input, rules, isPart2);
                        r2 = rules[r2n];
                    }
                    if (parts.Length == 2)
                    {
                        string[] part2numbers = parts[1].Split(' ');
                        int r3n = Convert.ToInt32(part2numbers[0]);

                        if (rules[r3n] == null)
                            r3 = Resolve(r3n, input, rules, isPart2);
                        r3 = rules[r3n];
                        if (part2numbers.Length == 2)
                        {
                            int r4n = Convert.ToInt32(part2numbers[1]);
                            if (rules[r4n] == null)
                                r4 = Resolve(r4n, input, rules, isPart2);
                            r4 = rules[r4n];
                        }
                    }
                    result = new Rule(ruleNumber, r1, r2, r3, r4);
                    rules[ruleNumber] = result;
                }
            }

            if (result == null)
                throw new InvalidOperationException();
            return result;
        }

        public override string? ToString()
        {
            if (IsFinal)
                return c.ToString();
            return number.ToString();
        }
    }
}

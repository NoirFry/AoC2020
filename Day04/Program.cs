using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Day04
{
    internal class Program
    {
        private const string InputFileName = @"..\..\..\Input.txt";
        private const string Cid = "cid";

        private static void Main()
        {
            string inputText = File.ReadAllText(InputFileName);
            string[] inputBlocks = inputText.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            int validCounterPart1 = 0;

            foreach (string block in inputBlocks)
            {
                int colons = CountOfColons(block);
                if (colons == 8
                    || (colons == 7 && !block.Contains(Cid)))
                {
                    validCounterPart1++;
                }
            }

            Console.WriteLine($"Part1: valid passports: {validCounterPart1}");

            int validCounterPart2 = 0;
            foreach (string s in inputBlocks)
            {
                int validSub = 0;
                string[] subBlocks = SplitInput(s);
                foreach (string sub in subBlocks)
                {
                    if (sub.StartsWith(Cid, StringComparison.Ordinal))
                        continue;
                    if (IsValid(sub))
                        validSub++;
                }
                if (validSub == 7)
                    validCounterPart2++;
            }

            Console.WriteLine($"Part2: valid passports: {validCounterPart2}");
            Console.ReadKey();
        }

        private static int CountOfColons(string str)
        {
            int counter = 0;
            foreach (char c in str)
            {
                if (c == ':')
                    counter++;
            }
            return counter;
        }

        private static string[] SplitInput(string str)
        {
            string[] splittedStr = str.Split(new[] { "\r\n", " ", }, StringSplitOptions.None);
            return splittedStr;
        }

        private static bool IsValid(string block)
        {
            if (block.StartsWith("byr", StringComparison.Ordinal))
            {
                int byr = Convert.ToInt32(block[4..], CultureInfo.InvariantCulture);
                if (byr < 1920 || byr > 2002)
                    return false;
            }
            else if (block.StartsWith("iyr", StringComparison.Ordinal))
            {
                int iyr = Convert.ToInt32(block[4..], CultureInfo.InvariantCulture);
                if (iyr < 2010 || iyr > 2020)
                    return false;
            }
            else if (block.StartsWith("eyr", StringComparison.Ordinal))
            {
                int eyr = Convert.ToInt32(block[4..], CultureInfo.InvariantCulture);
                if (eyr < 2020 || eyr > 2030)
                    return false;
            }
            else if (block.StartsWith("hgt", StringComparison.Ordinal))
            {
                if (!block.Contains("cm") && !block.Contains("in"))
                    return false;

                int height = Convert.ToInt32(block[4..^2], CultureInfo.InvariantCulture);
                if (block.Contains("cm")
                    && (height < 150 || height > 193))
                    return false;
                else if (block.Contains("in")
                    && (height < 59 || height > 76))
                    return false;
            }
            else if (block.StartsWith("hcl", StringComparison.Ordinal))
            {
                Match m = Regex.Match(block[4..], @"^#(\d|a|b|c|d|e|f){6}$");
                if (!m.Success)
                    return false;
            }
            else if (block.StartsWith("ecl", StringComparison.Ordinal))
            {
                string color = block[4..];
                if (color != "amb" && color != "blu" && color != "brn" && color != "gry" && color != "grn" && color != "hzl" && color != "oth")
                    return false;
            }
            else if (block.StartsWith("pid", StringComparison.Ordinal))
            {
                string id = block[4..];
                if (id.Length != 9 || !Regex.Match(id, @"^\d+$").Success)
                    return false;
            }
            else
            {
                Console.WriteLine("BAD BLOCK");
                return false;
            }

            return true;
        }
    }
}

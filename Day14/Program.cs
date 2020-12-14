using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            List<(int pos, ulong val)> mem = new();

            // mask1 - all zeros, we add 1s and shift
            ulong mask1 = 0;

            // mask0 - all ones, we add 0s and shift
            ulong mask0 = ulong.MaxValue;

            foreach (string line in inputLines)
            {
                ulong value = 0;
                int memAddress = 0;

                if (line[0..2] == "ma")
                {
                    mask1 = 0;
                    mask0 = ulong.MaxValue;
                    foreach (char symbol in line[7..])
                    {
                        if (symbol == '1')
                        {
                            mask1 <<= 1;
                            mask1 |= 1;
                            mask0 <<= 1;
                            mask0 |= 1;
                        }

                        if (symbol == '0')
                        {
                            mask1 <<= 1;
                            mask0 <<= 1;
                        }

                        if (symbol == 'X')
                        {
                            mask1 <<= 1;
                            mask0 <<= 1;
                            mask0 |= 1;
                        }
                    }
                }
                else
                {
                    string addressStr = line.Split(']', StringSplitOptions.None)[0][4..];
                    memAddress = Convert.ToInt32(addressStr);

                    string valueStr = line.Split(' ', StringSplitOptions.None)[2];
                    value = Convert.ToUInt64(valueStr);
                    value |= mask1;
                    value &= mask0;
                    if (HasMem(mem, memAddress))
                        RemoveMem(mem, memAddress);
                    mem.Add((memAddress, value));
                }
            }

            Console.WriteLine($"Part1: {mem.Sum(x => (decimal)x.val)} (right answer 7817357407588)");

            // Part 2

            List<(ulong pos, ulong val)> mem2 = new();

            string mask = string.Empty;
            foreach (string line in inputLines)
            {
                ulong value = 0;
                ulong memAddress = 0;
                if (line[0..2] == "ma")
                {
                    mask = line[7..];
                }
                else
                {
                    char[]? templateMask = mask.ToCharArray();
                    string addressStr = line.Split(']', StringSplitOptions.None)[0][4..];
                    memAddress = Convert.ToUInt64(addressStr);

                    string valueStr = line.Split(' ', StringSplitOptions.None)[2];
                    value = Convert.ToUInt64(valueStr);

                    for (int i = mask.Length - 1; i >= 0; i--)
                    {
                        if (templateMask[i] == '0')
                        {
                            if (memAddress % 2 == 0)
                                templateMask[i] = '0';
                            else
                                templateMask[i] = '1';
                        }

                        memAddress >>= 1;
                        if (memAddress == 0)
                            break;
                    }
                    //Console.WriteLine($"mask: {new string(templateMask)}");

                    char[] varMask = new char[templateMask.Length];
                    double count = Math.Pow(2, templateMask.Count(x => x == 'X'));

                    for (ulong i = 0; i < count; i++)
                    {
                        ulong t = i;
                        for (int j = 0; j < templateMask.Length; j++)
                        {
                            if (templateMask[j] == 'X')
                            {
                                varMask[j] = (t % 2) == 0 ? '0' : '1';
                                t >>= 1;
                            }
                            else
                                varMask[j] = templateMask[j];
                        }
                        //Console.WriteLine($"masks: {new string(varMask)}");
                        ulong address = Convert.ToUInt64(new string(varMask), 2);
                        if (HasMem(mem2, address))
                            RemoveMem(mem2, address);
                        mem2.Add((address, value));
                    }
                }
            }
            Console.WriteLine($"Part2: {mem2.Sum(x => (decimal)x.val)} (right answer 4335927555692)");

            Console.ReadKey();
        }

        private static bool HasMem(List<(int pos, ulong val)> mem, int pos)
        {
            return mem.Any(x => x.pos == pos);
        }

        private static bool HasMem(List<(ulong pos, ulong val)> mem, ulong pos)
        {
            return mem.Any(x => x.pos == pos);
        }

        private static void RemoveMem(List<(int pos, ulong val)> mem, int pos)
        {
            mem.RemoveAll(x => x.pos == pos);
        }
        private static void RemoveMem(List<(ulong pos, ulong val)> mem, ulong pos)
        {
            mem.RemoveAll(x => x.pos == pos);
        }
    }
}

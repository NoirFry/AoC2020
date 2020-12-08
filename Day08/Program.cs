namespace Day08
{
    using HgcEmulator;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = File.ReadAllLines(InputFileName);

            List<HgcOpCodes> program = new();
            List<int> args = new();

            foreach (string line in inputLines)
            {
                string[] blocks = line.Split(' ', StringSplitOptions.None);
                program.Add(Enum.Parse<HgcOpCodes>(blocks[0]));
                args.Add(Convert.ToInt32(blocks[1], CultureInfo.InvariantCulture));
            }

            HgcEmulator emulator = new HgcEmulator(program, args);

            if (!emulator.Run(out int part1Acc))
            {
                Console.WriteLine($"Part1: accumulator: {part1Acc}, (right answer: 1384)");
            }

            for (int i = 0; i < program.Count; i++)
            {
                emulator.Reset();

                emulator.InstructionToggleNopJmp(i);

                bool exited = emulator.Run(out int acc);
                if (!exited)
                {
                    emulator.InstructionToggleNopJmp(i);
                }
                else
                {
                    Console.WriteLine($"Part2: accumulator: {acc}, (right answer: 761)");
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}

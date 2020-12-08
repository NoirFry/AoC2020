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

            List<HgcOpCode> program = new();
            List<int> args = new();

            foreach (string line in inputLines)
            {
                string[] blocks = line.Split(' ', StringSplitOptions.None);
                program.Add(Enum.Parse<HgcOpCode>(blocks[0]));
                args.Add(Convert.ToInt32(blocks[1], CultureInfo.InvariantCulture));
            }

            HgcEmulator emulator = new HgcEmulator(program, args);

            emulator.Run();

            Console.WriteLine($"Part1: accumulator: {emulator.Accumulator}, (right answer: 1384)");

            for (int i = 0; i < program.Count; i++)
            {
                emulator.InstructionToggleNopJmp(i);

                ReturnStatus status = emulator.Run();
                if (status == ReturnStatus.Finished)
                {
                    Console.WriteLine($"Part2: accumulator: {emulator.Accumulator}, (right answer: 761)");
                    break;
                }

                emulator.InstructionToggleNopJmp(i);
            }

            Console.ReadKey();
        }
    }
}

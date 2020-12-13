using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Day12
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = File.ReadAllLines(InputFileName);

            List<Instr> instructions = new();

            foreach (string line in inputLines)
            {
                int distance = ToInt(line[1..]);

                instructions.Add(new Instr(Enum.Parse<ShipAction>(line[0].ToString()), distance));
            }

            ShipV1 ship = new();

            foreach (Instr instr in instructions)
            {
                ship.Execute(instr);
            }

            Console.WriteLine($"Part1: x: {ship.PosX}, y: {ship.PosY}, distance: {Math.Abs(ship.PosX) + Math.Abs(ship.PosY)} (right answer: 1010)");

            ShipV2 shipv2 = new();

            foreach (Instr instr in instructions)
            {
                shipv2.Execute(instr);
            }

            Console.WriteLine($"Part2: x: {shipv2.PosX}, y: {shipv2.PosY}, distance: {Math.Abs(shipv2.PosX) + Math.Abs(shipv2.PosY)} (right answer: 52742)");

            Console.ReadKey();
        }

        private static int ToInt(string str)
        {
            return Convert.ToInt32(str, CultureInfo.InvariantCulture);
        }
    }
}

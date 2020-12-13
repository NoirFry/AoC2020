using System;

namespace Day12
{
    internal class ShipV2
    {
        private readonly NavPoint navPoint;

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        public ShipV2()
        {
            PosX = 0;
            PosY = 0;
            navPoint = new NavPoint(10, 1);
        }

        public void Execute(Instr instruction)
        {
            if (instruction.Action == ShipAction.F)
            {
                PosX += navPoint.X * instruction.Distance;
                PosY += navPoint.Y * instruction.Distance;
            }
            else if (instruction.Action == ShipAction.L || instruction.Action == ShipAction.R)
            {
                int rot = instruction.Distance;
                if (instruction.Action == ShipAction.L)
                {
                    rot = (360 - instruction.Distance) % 360;
                }
                navPoint.Rotate(rot);
            }
            else
            {
                navPoint.Move(instruction.Action, instruction.Distance);
            }
        }
    }
}

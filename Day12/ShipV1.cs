using System;

namespace Day12
{
    internal class ShipV1
    {
        private int _rot;

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        private int Rot
        {
            get => _rot;
            set
            {
                _rot = (value) % 360;
                if (_rot < 0)
                    _rot += 360;
            }
        }

        public ShipV1()
        {
            Rot = 90;
            PosX = 0;
            PosY = 0;
        }

        public void Execute(Instr instruction)
        {
            if (instruction.Action == ShipAction.F)
            {
                Execute(new Instr(ToShipAction(Rot), instruction.Distance));
                return;
            }

            switch (instruction.Action)
            {
                case ShipAction.N:
                    PosY -= instruction.Distance;
                    break;
                case ShipAction.E:
                    PosX += instruction.Distance;
                    break;
                case ShipAction.S:
                    PosY += instruction.Distance;
                    break;
                case ShipAction.W:
                    PosX -= instruction.Distance;
                    break;
                case ShipAction.L:
                    Rot -= instruction.Distance;
                    break;
                case ShipAction.R:
                    Rot += instruction.Distance;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static ShipAction ToShipAction(int rot)
        {
            return rot switch
            {
                0 => ShipAction.N,
                90 => ShipAction.E,
                180 => ShipAction.S,
                270 => ShipAction.W,
                _ => throw new NotImplementedException(),
            };
        }
    }
}

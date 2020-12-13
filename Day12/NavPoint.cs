using System;

namespace Day12
{
    internal class NavPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public NavPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Rotate(int rot)
        {
            switch (rot)
            {
                case 90:
                    int oldX = X;
                    X = Y;
                    Y = -oldX;
                    break;
                case 180:
                    X = -X;
                    Y = -Y;
                    break;
                case 270:
                    int oldY = Y;
                    Y = X;
                    X = -oldY;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        internal void Move(ShipAction action, int distance)
        {
            switch (action)
            {
                case ShipAction.N:
                    Y += distance;
                    break;
                case ShipAction.E:
                    X += distance;
                    break;
                case ShipAction.S:
                    Y -= distance;
                    break;
                case ShipAction.W:
                    X -= distance;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

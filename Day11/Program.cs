using System;
using System.IO;

namespace Day11
{
    internal class Program
    {
        private static int maxX = 0;
        private static int maxY = 0;
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = File.ReadAllLines(InputFileName);

            maxX = inputLines[0].Length + 2;
            maxY = inputLines.Length + 2;

            Seat[,] map = new Seat[maxX, maxY];
            Seat[,] mapNext = new Seat[maxX, maxY];

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (IsBorder(x, y))
                        map[x, y] = Seat.Floor;
                    else
                        map[x, y] = CharToSeat(inputLines[y - 1][x - 1]);

                }
            }

            int changes;
            int step = 0;

            do
            {
                changes = 0;

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        if (IsBorder(x, y))
                            mapNext[x, y] = Seat.Floor;
                        else
                        {
                            mapNext[x, y] = NextStepPart1(map, x, y);
                            if (map[x, y] != mapNext[x, y])
                                changes++;
                        }
                    }
                }

                Seat[,] t = map;
                map = mapNext;
                mapNext = t;
                step++;
            } while (changes != 0);

            int occSeats = 0;
            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    if (map[j, i] == Seat.Occupied)
                        occSeats++;
                }
            }

            Console.WriteLine($"Part1: occupied seats: {occSeats} (right answer: 2344)");

            // Part 2
            step = 0;
            occSeats = 0;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (IsBorder(x, y))
                        map[x, y] = Seat.Floor;
                    else
                        map[x, y] = CharToSeat(inputLines[y - 1][x - 1]);

                }
            }

            do
            {
                changes = 0;

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        if (IsBorder(x, y))
                            mapNext[x, y] = Seat.Floor;
                        else
                        {
                            mapNext[x, y] = NextStepPart2(map, x, y);
                            if (map[x, y] != mapNext[x, y])
                                changes++;
                        }
                    }
                }

                Seat[,] t = map;
                map = mapNext;
                mapNext = t;
                step++;
            } while (changes != 0);

            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    if (map[j, i] == Seat.Occupied)
                        occSeats++;
                }
            }

            Console.WriteLine($"Part2: occupied seats: {occSeats} (right answer: 2076)");




            Console.ReadKey();
        }

        private static bool IsBorder(int x, int y)
        {
            return x == 0 || x == maxX - 1 || y == 0 || y == maxY - 1;
        }

        private static Seat NextStepPart1(Seat[,] map, int x, int y)
        {
            if (map[x, y] == Seat.Floor)
                return Seat.Floor;

            int occCount = GetOccupiedCountPart1(map, x, y);

            if (map[x, y] == Seat.Empty && occCount == 0)
                return Seat.Occupied;
            if (map[x, y] == Seat.Occupied && occCount >= 4)
                return Seat.Empty;
            return map[x, y];
        }

        private static Seat NextStepPart2(Seat[,] map, int x, int y)
        {
            if (map[x, y] == Seat.Floor)
                return Seat.Floor;

            int occCount = GetOccupiedCountPart2(map, x, y);

            if (map[x, y] == Seat.Empty && occCount == 0)
                return Seat.Occupied;
            if (map[x, y] == Seat.Occupied && occCount >= 5)
                return Seat.Empty;
            return map[x, y];
        }

        private static int GetOccupiedCountPart1(Seat[,] map, int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    if (map[x + j, y + i] == Seat.Occupied)
                        count++;
                }
            }
            return count;
        }

        private static int GetOccupiedCountPart2(Seat[,] map, int x, int y)
        {
            int count = 0;
            for (int direction = 0; direction < 8; direction++)
            {
                bool occupied = IsSeatInDirection(map, x, y, direction);
                if (occupied)
                    count++;
            }
            return count;
        }

        private static bool IsSeatInDirection(Seat[,] map, int x, int y, int direction)
        {
            int curX = x;
            int curY = y;
            while (!IsBorder(curX, curY))
            {
                GetNextPos(curX, curY, out curX, out curY, direction);
                if (map[curX, curY] == Seat.Occupied)
                    return true;
                if (map[curX, curY] == Seat.Empty)
                    return false;

            }
            return false;
        }

        private static void GetNextPos(int curX, int curY, out int newX, out int newY, int direction)
        {
            switch (direction)
            {
                case 0:
                    newX = curX;
                    newY = curY - 1;
                    break;
                case 1:
                    newX = curX + 1;
                    newY = curY - 1;
                    break;
                case 2:
                    newX = curX + 1;
                    newY = curY;
                    break;
                case 3:
                    newX = curX + 1;
                    newY = curY + 1;
                    break;
                case 4:
                    newX = curX;
                    newY = curY + 1;
                    break;
                case 5:
                    newX = curX - 1;
                    newY = curY + 1;
                    break;
                case 6:
                    newX = curX - 1;
                    newY = curY;
                    break;
                case 7:
                    newX = curX - 1;
                    newY = curY - 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static Seat CharToSeat(char c)
        {
            return c switch
            {
                '.' => Seat.Floor,
                'L' => Seat.Empty,
                '#' => Seat.Occupied,
                _ => throw new NotImplementedException(),
            };
        }
    }

    internal enum Seat
    {
        Floor,
        Empty,
        Occupied,
    }
}

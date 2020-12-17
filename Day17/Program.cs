using System;

namespace Day17
{
    internal class Program
    {
        private static void Main()
        {
            int Offset = 20;

            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            bool[,,] pd = new bool[40, 40, 40];
            bool[,,] pdNext = new bool[40, 40, 40];

            for (int y = Offset; y < Offset + 8; y++)
            {
                for (int x = Offset; x < Offset + 8; x++)
                {
                    if (inputLines[y - Offset][x - Offset] == '#')
                    {
                        pd[x, y, Offset] = true;
                        //Console.Write('#');
                    }
                    else
                    {
                        //Console.Write('.');
                    }
                }
                //Console.WriteLine();
            }

            //Console.WriteLine($"Step: {0} active: {ActiveCount(pd)}, {GetNeighborCount(pd, 11, 10, 10)}");


            for (int step = 0; step < 6; step++)
            {
                for (int i = 1; i < 39; i++)
                {
                    for (int j = 1; j < 39; j++)
                    {
                        for (int k = 1; k < 39; k++)
                        {
                            int n = GetNeighborCount3D(pd, k, j, i);
                            if (pd[k, j, i])
                            {
                                if (n == 2 || n == 3)
                                {
                                    pdNext[k, j, i] = true;
                                    //Console.Write('#');
                                }
                                else
                                {
                                    pdNext[k, j, i] = false;
                                    //Console.Write('.');
                                }

                            }
                            else
                            {
                                if (n == 3)
                                {
                                    pdNext[k, j, i] = true;
                                    //Console.Write('#');
                                }
                                else
                                {
                                    pdNext[k, j, i] = false;
                                    //Console.Write('.');
                                }
                            }
                        }
                        //Console.WriteLine();
                    }
                    //Console.WriteLine($"Level z: {i}");
                }

                //Console.WriteLine($"Step: {step + 1} active: {ActiveCount(pdNext)}");
                bool[,,] t = pd;
                pd = pdNext;
                pdNext = t;
            }


            Console.WriteLine($"Part1: {ActiveCount3D(pd)} (right answer: 286)");

            // Part 2
            Offset = 8;
            bool[,,,] pd4d = new bool[24, 24, 24, 24];
            bool[,,,] pd4dNext = new bool[24, 24, 24, 24];

            for (int y = Offset; y < Offset + 8; y++)
            {
                for (int x = Offset; x < Offset + 8; x++)
                {
                    if (inputLines[y - Offset][x - Offset] == '#')
                    {
                        pd4d[x, y, Offset, Offset] = true;
                        //Console.Write('#');
                    }
                    else
                    {
                        //Console.Write('.');
                    }
                }
                //Console.WriteLine();
            }

            //Console.WriteLine($"Step: {0} active: {ActiveCount(pd)}, {GetNeighborCount(pd, 11, 10, 10)}");


            for (int step = 0; step < 6; step++)
            {
                for (int l = 1; l < 23; l++)
                    for (int i = 1; i < 23; i++)
                    {
                        for (int j = 1; j < 23; j++)
                        {
                            for (int k = 1; k < 23; k++)
                            {
                                int n = GetNeighborCount4D(pd4d, k, j, i, l);
                                if (pd4d[k, j, i, l])
                                {
                                    if (n == 2 || n == 3)
                                    {
                                        pd4dNext[k, j, i, l] = true;
                                        //Console.Write('#');
                                    }
                                    else
                                    {
                                        pd4dNext[k, j, i, l] = false;
                                        //Console.Write('.');
                                    }

                                }
                                else
                                {
                                    if (n == 3)
                                    {
                                        pd4dNext[k, j, i, l] = true;
                                        //Console.Write('#');
                                    }
                                    else
                                    {
                                        pd4dNext[k, j, i, l] = false;
                                        //Console.Write('.');
                                    }
                                }
                            }
                            //Console.WriteLine();
                        }
                        //Console.WriteLine($"Level z: {i}");
                    }

                //Console.WriteLine($"Step: {step + 1} active: {ActiveCount(pdNext)}");
                bool[,,,] t = pd4d;
                pd4d = pd4dNext;
                pd4dNext = t;
            }


            Console.WriteLine($"Part2: {ActiveCount4D(pd4d)} (right answer: 960)");
            Console.ReadKey();
        }

        private static int GetNeighborCount4D(bool[,,,] pd4d, int x, int y, int z, int a)
        {
            int count = 0;
            for (int l = a - 1; l < a + 2; l++)
                for (int i = z - 1; i < z + 2; i++)
                    for (int j = y - 1; j < y + 2; j++)
                        for (int k = x - 1; k < x + 2; k++)
                        {
                            if (i == z && j == y && k == x && l == a)
                                continue;
                            if (pd4d[k, j, i, l])
                                count++;
                        }
            return count;
        }

        private static int ActiveCount4D(bool[,,,] pd4d)
        {
            int count = 0;
            for (int l = 0; l < 24; l++)
                for (int i = 0; i < 24; i++)
                    for (int j = 0; j < 24; j++)
                        for (int k = 0; k < 24; k++)
                        {
                            if (pd4d[k, j, i, l])
                                count++;
                        }
            return count;
        }

        private static int GetNeighborCount3D(bool[,,] pd, int x, int y, int z)
        {
            int count = 0;
            for (int i = z - 1; i < z + 2; i++)
                for (int j = y - 1; j < y + 2; j++)
                    for (int k = x - 1; k < x + 2; k++)
                    {
                        if (i == z && j == y && k == x)
                            continue;
                        if (pd[k, j, i])
                            count++;
                    }
            return count;
        }

        private static int ActiveCount3D(bool[,,] pd)
        {
            int count = 0;
            for (int i = 0; i < 40; i++)
                for (int j = 0; j < 40; j++)
                    for (int k = 0; k < 40; k++)
                    {
                        if (pd[k, j, i])
                            count++;
                    }
            return count;
        }
    }
}

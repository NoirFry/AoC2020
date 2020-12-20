using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    internal class Program
    {
        private static void Main()
        {
            string InputFileName = @"..\..\..\Input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(InputFileName);

            List<Tile> tiles = new();

            int tileNumber = 0;
            for (int i = 0; i < inputLines.Length; i++)
            {
                string line = inputLines[i];
                if (line.StartsWith("Tile"))
                {
                    tileNumber = Convert.ToInt32(line.Split(' ')[1][..^1]);
                }
                else if (line.StartsWith('.') || line.StartsWith('#'))
                {
                    bool[,] map = new bool[Tile.Size, Tile.Size];
                    for (int j = 0; j < Tile.Size; j++)
                    {
                        string tileLine = inputLines[i + j];
                        for (int k = 0; k < Tile.Size; k++)
                        {
                            if (tileLine[k] == '#')
                                map[k, j] = true;
                        }
                    }
                    i += line.Length;
                    tiles.Add(new Tile(map, tileNumber));
                }
            }

            List<Tile> cornerTiles = new();

            foreach (Tile tile in tiles)
            {
                int borderCount = 0;
                for (int i = 0; i < 4; i++)
                {
                    bool[] top = tile.GetTopBorder();
                    if (Tile.IsBorder(top, tiles, tile))
                        borderCount++;
                    tile.Rotate();
                }
                if (borderCount == 2)
                    cornerTiles.Add(tile);
            }

            long part1 = 1;
            foreach (Tile tile in cornerTiles)
            {
                part1 *= tile.TileNumber;
            }

            Console.WriteLine($"Part1: {part1} (right answer: 21599955909991)");
            Console.WriteLine($"count: {cornerTiles.Count}");

            // Part 2

            Picture pic = new Picture(cornerTiles[0], tiles);
            for (int y = 0; y < Picture.ImageSize; y++)
            {
                for (int x = 0; x < Picture.ImageSize; x++)
                {
                    Console.Write(pic.image[y, x] ? '#' : '.');
                }
                Console.WriteLine();
            }

            for (int y = 0; y < Picture.ImageSize; y++)
            {
                for (int x = 0; x < Picture.ImageSize; x++)
                {
                    if (pic.HasMonsterAt(x, y))
                    {
                        Console.WriteLine($"Monster at: {x}, {y}");
                    }
                }
            }

            bool found;
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($"Try: {i}");
                found = false;
                for (int y = 0; y < Picture.ImageSize; y++)
                {
                    for (int x = 0; x < Picture.ImageSize; x++)
                    {
                        if (pic.HasMonsterAt(x, y))
                        {
                            found = true;
                            Console.WriteLine($"Monster at: {x}, {y}");
                            pic.RemoveMonsterAt(x, y);
                        }
                    }
                }
                if (found)
                    break;
                if ((i % 2) == 0)
                    pic.Flip();
                else
                {
                    pic.Flip();
                    pic.Rotate();
                }
            }

            int part2 = 0;

            for (int y = 0; y < Picture.ImageSize; y++)
            {
                for (int x = 0; x < Picture.ImageSize; x++)
                {
                    if (pic.image[x, y])
                        part2++;
                }
            }


            Console.WriteLine($"Part2: {part2} (right answer: 2495)");
            Console.ReadKey();
        }
    }
}

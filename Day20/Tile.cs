using System.Collections.Generic;

namespace Day20
{
    internal class Tile
    {
        public const int Size = 10;

        private readonly int tileNumber;

        private readonly bool[,] map;

        private bool isFlipped;

        private int rotation;

        public int TileNumber => tileNumber;

        public bool IsFlipped => isFlipped;

        public int Rotation => rotation;

        public bool IsUsed { get; set; }

        public Tile(bool[,] map, int tileNumber)
        {
            this.map = map;
            this.tileNumber = tileNumber;
        }

        public bool[] GetTopBorder()
        {
            bool[] result = new bool[Size];

            for (int x = 0; x < Size; x++)
                result[x] = map[x, 0];

            return result;
        }

        public bool[] GetBottomBorder()
        {
            bool[] result = new bool[Size];

            for (int x = 0; x < Size; x++)
                result[x] = map[x, Size - 1];

            return result;
        }

        public bool[] GetRightBorder()
        {
            bool[] result = new bool[Size];
            for (int y = 0; y < Size; y++)
                result[y] = map[Size - 1, y];

            return result;
        }

        public bool[] GetLeftBorder()
        {
            bool[] result = new bool[Size];
            for (int y = 0; y < Size; y++)
                result[y] = map[0, y];

            return result;
        }

        public void Flip()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size / 2; x++)
                {
                    bool t = map[x, y];
                    map[x, y] = map[Size - x - 1, y];
                    map[Size - x - 1, y] = t;
                }
            }
            isFlipped = !isFlipped;
        }

        public void Rotate(int rot = 1)
        {
            if (IsFlipped)
                Flip();
            bool[,] result = new bool[Size, Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    result[y, Size - 1 - x] = map[x, y];
                }
            }
            Copy(result);
            rotation = (rotation + 1) % 4;

            if (rot > 1)
                Rotate(rot - 1);
        }

        public bool IsAdjustTo(Tile tile)
        {
            if (this == tile)
                return false;

            bool[] top1;
            bool[] top2;
            for (int i = 0; i < 4; i++)
            {
                top1 = GetTopBorder();
                for (int j = 0; j < 8; j++)
                {
                    top2 = tile.GetTopBorder();
                    if (Match(top1, top2))
                        return true;
                    if (j % 2 == 0)
                        tile.Flip();
                    else
                    {
                        tile.Flip();
                        tile.Rotate();
                    }
                }
                Rotate();
            }
            return false;
        }

        public static bool Match(bool[] a, bool[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        public bool[] GetInsideRow(int y)
        {
            bool[] result = new bool[Size - 2];
            int relY = y + 1;
            for (int x = 0; x < Size - 2; x++)
            {
                int relX = x + 1;
                result[x] = map[relX, relY];
            }
            return result;
        }

        private void Copy(bool[,] newMap)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    map[x, y] = newMap[x, y];
                }
            }
        }

        public override string ToString()
        {
            return tileNumber.ToString();
        }

        public static bool IsBorder(bool[] border, List<Tile> tiles, Tile currentTile)
        {
            foreach (Tile tile in tiles)
            {
                if (tile == currentTile)
                    continue;

                for (int i = 0; i < 8; i++)
                {
                    bool[] top = tile.GetTopBorder();
                    if (Tile.Match(border, top))
                        return false;
                    if (i % 2 == 0)
                        tile.Flip();
                    else
                    {
                        tile.Flip();
                        tile.Rotate();
                    }
                }
            }
            return true;
        }
    }
}

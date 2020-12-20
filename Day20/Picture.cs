using System;
using System.Collections.Generic;

namespace Day20
{
    internal class Picture
    {
        public const int Size = 12;

        public const int ImageSize = (Tile.Size - 2) * Size;

        public readonly bool[,] image = new bool[ImageSize, ImageSize];

        private readonly Tile[,] tiles = new Tile[Size, Size];

        public Picture(Tile tile, List<Tile> inputTiles)
        {
            tiles[0, 0] = tile;
            tile.IsUsed = true;
            ArrangeTiles(inputTiles);
            ConstructImage();
        }

        private void ArrangeTiles(List<Tile> inputTiles)
        {
            Tile t0 = tiles[0, 0];
            if (t0.IsFlipped)
                t0.Flip();
            bool[] top = t0.GetTopBorder();
            if (Tile.IsBorder(top, inputTiles, t0))
            {
                t0.Rotate();
                top = t0.GetTopBorder();
                if (!Tile.IsBorder(top, inputTiles, t0))
                    t0.Rotate(3);
            }
            else
            {
                t0.Rotate(3);
                top = t0.GetTopBorder();
                if (!Tile.IsBorder(top, inputTiles, t0))
                    t0.Rotate(3);
            }

            for (int y = 0; y < Size; y++)
            {
                Tile? result;
                for (int x = 1; x < Size; x++)
                {
                    bool[] right = tiles[x - 1, y].GetRightBorder();
                    result = null;
                    foreach (Tile tile in inputTiles)
                    {
                        if (tile.IsUsed)
                            continue;
                        for (int i = 0; i < 8; i++)
                        {
                            bool[] left = tile.GetLeftBorder();
                            if (Tile.Match(right, left))
                            {
                                result = tile;
                                tile.IsUsed = true;
                                break;
                            }
                            if ((i % 2) == 0)
                                tile.Flip();
                            else
                            {
                                tile.Flip();
                                tile.Rotate();
                            }

                        }
                        if (result != null)
                            break;
                    }
                    if (result == null)
                        throw new NotImplementedException();
                    tiles[x, y] = result;
                }

                if (y == (Size - 1))
                    break;

                bool[] bottom = tiles[0, y].GetBottomBorder();
                result = null;
                foreach (Tile tile in inputTiles)
                {
                    if (tile.IsUsed)
                        continue;
                    for (int i = 0; i < 8; i++)
                    {
                        top = tile.GetTopBorder();
                        if (Tile.Match(top, bottom))
                        {
                            result = tile;
                            tile.IsUsed = true;
                            break;
                        }
                        if ((i % 2) == 0)
                            tile.Flip();
                        else
                        {
                            tile.Flip();
                            tile.Rotate();
                        }
                    }
                    if (result != null)
                        break;
                }
                if (result == null)
                    throw new NotImplementedException();
                tiles[0, y + 1] = result;
            }
        }

        private void ConstructImage()
        {
            int globalX = 0, globalY = 0;
            for (int tileY = 0; tileY < Size; tileY++)
            {
                for (int tileX = 0; tileX < Size; tileX++)
                {
                    for (int y = 0; y < Tile.Size - 2; y++)
                    {
                        bool[] row = tiles[tileX, tileY].GetInsideRow(y);
                        for (int x = 0; x < Tile.Size - 2; x++)
                        {
                            bool pixel = row[x];
                            image[globalX + x, globalY + y] = pixel;
                        }
                    }
                    globalX += Tile.Size - 2;
                }
                globalY += Tile.Size - 2;
                globalX = 0;
            }
        }

        public bool HasMonsterAt(int x, int y)
        {
            if (x + 19 >= ImageSize || y + 3 >= ImageSize)
                return false;
            foreach ((int x, int y) monCoord in GetMonsterCoords())
            {
                if (!image[x + monCoord.x, y + monCoord.y])
                    return false;
            }
            return true;
        }

        public void RemoveMonsterAt(int x, int y)
        {
            foreach ((int x, int y) monCoord in GetMonsterCoords())
            {
                image[x + monCoord.x, y + monCoord.y] = false;
            }
        }

        public void Flip()
        {
            for (int y = 0; y < ImageSize; y++)
            {
                for (int x = 0; x < ImageSize / 2; x++)
                {
                    bool t = image[x, y];
                    image[x, y] = image[ImageSize - x - 1, y];
                    image[ImageSize - x - 1, y] = t;
                }
            }
        }

        public void Rotate()
        {
            bool[,] result = new bool[ImageSize, ImageSize];
            for (int y = 0; y < ImageSize; y++)
            {
                for (int x = 0; x < ImageSize; x++)
                {
                    result[y, ImageSize - 1 - x] = image[x, y];
                }
            }
            Copy(result);
        }

        private void Copy(bool[,] newMap)
        {
            for (int y = 0; y < ImageSize; y++)
            {
                for (int x = 0; x < ImageSize; x++)
                {
                    image[x, y] = newMap[x, y];
                }
            }
        }

        /// <summary>
        ///01234567890123456789
        ///                  # 
        ///#    ##    ##    ###
        /// #  #  #  #  #  #   
        ///01234567890123456789
        /// </summary>
        private List<(int x, int y)> GetMonsterCoords()
        {
            List<(int x, int y)> list = new List<(int x, int y)>
            {
                (18,0),
                (0,1),
                (5,1),
                (6,1),
                (11,1),
                (12,1),
                (17,1),
                (18,1),
                (19,1),
                (1,2),
                (4,2),
                (7,2),
                (10,2),
                (13,2),
                (16,2),
            };
            return list;
        }
    }
}

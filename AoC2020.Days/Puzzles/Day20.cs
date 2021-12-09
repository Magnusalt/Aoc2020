using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day20 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day20));
            //var input = ReadTestInput(nameof(Day20));

            var tiles = new List<Tile>();
            var currImage = new Tile();
            foreach (var s in input)
            {
                if (s.Contains("Tile"))
                {
                    currImage.Id = int.Parse(s.Substring(5, 4));
                    continue;
                }

                if (string.IsNullOrEmpty(s))
                {
                    tiles.Add(currImage);
                    currImage = new Tile();
                    continue;
                }

                currImage.ImageData.Add(s);
            }

            tiles.Add(currImage);

            var result = 1L;
            var count = 0;
            foreach (var tile in tiles)
            {
                if (IsCorner(tile, tiles))
                {
                    result *= tile.Id;
                    count++;
                }

                if (count == 4) break;
            }


            Console.WriteLine(result);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day20));
            //var input = ReadTestInput(nameof(Day20));

            var tiles = new List<TileFactory>();
            var currImage = new TileFactory();
            foreach (var s in input)
            {
                if (s.Contains("Tile"))
                {
                    currImage.Id = int.Parse(s.Substring(5, 4));
                    continue;
                }

                if (string.IsNullOrEmpty(s))
                {
                    tiles.Add(currImage);
                    currImage = new TileFactory();
                    continue;
                }

                currImage.ImageData.Add(s);
            }

            tiles.Add(currImage);

            var size = (int) Math.Sqrt(tiles.Count);

            var x = 0;
            var y = 0;

            var puzzle = new (int, List<Tile>)[size, size];

            var queue = new Queue<(int, List<Tile>)>();

            foreach (var tileFactory in tiles)
            {
                var list = new List<Tile>();
                for (int i = 0; i < 8; i++)
                {
                    var nextTile = tileFactory.GetNextState();
                    list.Add(nextTile);
                }

                queue.Enqueue((0, list));
            }

            var tested = new Queue<(int, List<Tile>)>();
            while (puzzle[size - 1, size - 1].Item2 == null)
            {
                while (queue.Count > 0)
                {
                    if (puzzle[x, y].Item2 == null)
                    {
                        var t = queue.Dequeue();
                        if (y == 0 && x == 0)
                        {
                            puzzle[0, 0] = t;
                            x++;
                            continue;
                        }

                        var added = false;
                        for (int i = t.Item1; i < 8; i++)
                        {
                            switch (y)
                            {
                                case 0 when x > 0:
                                {
                                    var (item1, left) = puzzle[x - 1, y];
                                    if (left[item1].RightBorder == t.Item2[i].LeftBorder)
                                    {
                                        puzzle[x, y] = (i, t.Item2);
                                        added = true;
                                    }

                                    break;
                                }
                                case > 0 when x == 0:
                                    var above = puzzle[x, y - 1];
                                    if (above.Item2[above.Item1].BottomBorder == t.Item2[i].TopBorder)
                                    {
                                        puzzle[x, y] = (i, t.Item2);
                                        added = true;
                                    }

                                    break;
                                case > 0 when x > 0:
                                    var lefty = puzzle[x - 1, y];
                                    var aboveie = puzzle[x, y - 1];
                                    if (aboveie.Item2[aboveie.Item1].BottomBorder == t.Item2[i].TopBorder &&
                                        lefty.Item2[lefty.Item1].RightBorder == t.Item2[i].LeftBorder)
                                    {
                                        puzzle[x, y] = (i, t.Item2);
                                        added = true;
                                    }

                                    break;
                            }

                            if (added)
                            {
                                break;
                            }
                        }

                        if (!added)
                        {
                            tested.Enqueue((0, t.Item2));
                        }
                        else
                        {
                            if (x < size - 1)
                            {
                                x++;
                            }
                            else
                            {
                                y++;
                                x = 0;
                            }

                            while (tested.Count > 0)
                            {
                                var qi = tested.Dequeue();
                                queue.Enqueue(qi);
                            }
                        }
                    }
                    else
                    {
                        var added = false;
                        var t1 = puzzle[x, y];
                        if (x == 0 && y == 0 && t1.Item1 < 7)
                        {
                            puzzle[x, y] = (t1.Item1 + 1, t1.Item2);
                            added = true;
                        }

                        if (!added)
                        {
                            for (int i = t1.Item1 + 1; i < 8; i++)
                            {
                                switch (y)
                                {
                                    case 0 when x > 0:
                                    {
                                        var (item1, left) = puzzle[x - 1, y];
                                        if (left[item1].RightBorder == t1.Item2[i].LeftBorder)
                                        {
                                            puzzle[x, y] = (i, t1.Item2);
                                            added = true;
                                        }

                                        break;
                                    }
                                    case > 0 when x == 0:
                                        var above = puzzle[x, y - 1];
                                        if (above.Item2[above.Item1].BottomBorder == t1.Item2[i].TopBorder)
                                        {
                                            puzzle[x, y] = (i, t1.Item2);
                                            added = true;
                                        }

                                        break;
                                    case > 0 when x > 0:
                                        var lefty = puzzle[x - 1, y];
                                        var aboveie = puzzle[x, y - 1];
                                        if (aboveie.Item2[aboveie.Item1].BottomBorder == t1.Item2[i].TopBorder &&
                                            lefty.Item2[lefty.Item1].RightBorder == t1.Item2[i].LeftBorder)
                                        {
                                            puzzle[x, y] = (i, t1.Item2);
                                            added = true;
                                        }

                                        break;
                                }

                                if (added)
                                {
                                    break;
                                }
                            }
                        }

                        if (!added)
                        {
                            tested.Enqueue((0, t1.Item2));
                            puzzle[x, y] = default;
                            if (x > 0)
                            {
                                x--;
                            }
                            else if (x == 0 && y > 0)
                            {
                                x = size - 1;
                                y--;
                            }
                        }
                        else
                        {
                            if (x < size - 1)
                            {
                                x++;
                            }
                            else
                            {
                                y++;
                                x = 0;
                            }
                        }
                    }
                }

                if (x > 0)
                {
                    x--;
                }
                else if (x == 0 && y > 0)
                {
                    x = size - 1;
                    y--;
                }

                while (tested.Count > 0)
                {
                    var qi = tested.Dequeue();
                    queue.Enqueue(qi);
                }
            }

            Console.Clear();

            var finalTileImageData = Enumerable.Repeat("", size * 8).ToList();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var (index, tileList) = puzzle[j, i];
                    var tile = tileList[index].ImageData;
                    var row = i * 8;
                    foreach (var s in tile.Skip(1).Take(8))
                    {
                        finalTileImageData[row] += new string(s.Skip(1).Take(8).ToArray());
                        row++;
                    }
                }
            }

            var finalAsTile = new TileFactory {ImageData = finalTileImageData};
            var allHash = finalAsTile.ImageData.SelectMany(id => id).Count(c => c == '#');

            for (int _ = 0; _ < 8; _++)
            {
                finalAsTile.GetNextState();
                var seaMonster = new List<string>
            {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };

                

                var seaMonsterLength = seaMonster.First().Length;
                var seaMonsterHeight = 3;

                var imageWidth = finalAsTile.ImageData.First().Length;

                var seaMonsterCount = 0;

                for (int i = 0; i < finalAsTile.ImageData.Count - seaMonsterHeight; i++)
                {
                    for (int j = 0; j < imageWidth - seaMonsterLength; j++)
                    {
                        var row1 = finalAsTile.ImageData[i].Substring(j, seaMonsterLength);
                        var row2 = finalAsTile.ImageData[i + 1].Substring(j, seaMonsterLength);
                        var row3 = finalAsTile.ImageData[i + 2].Substring(j, seaMonsterLength);
                        var r1Result = new List<bool>();
                        for (var c = 0; c < row1.Length; c++)
                        {
                            if (seaMonster[0][c] == '#' && row1[c] == '#')
                            {
                                r1Result.Add(true);
                            }

                            if (seaMonster[0][c] == '#' && row1[c] != '#')
                            {
                                r1Result.Add(false);
                            }


                            if (seaMonster[1][c] == '#' && row2[c] == '#')
                            {
                                r1Result.Add(true);
                            }
                            if (seaMonster[1][c] == '#' && row2[c] != '#')
                            {
                                r1Result.Add(false);
                            }


                            if (seaMonster[2][c] == '#' && row3[c] == '#')
                            {
                                r1Result.Add(true);
                            }

                            if (seaMonster[2][c] == '#' && row3[c] != '#')
                            {
                                r1Result.Add(false);
                            }
                        }

                        if (r1Result.All(b => b))
                        {
                            seaMonsterCount++;
                        }
                    }
                }

                if (seaMonsterCount > 0)
                {
                    var monsterHash = seaMonster.SelectMany(s => s).Count(c => c == '#');

                    Console.WriteLine(allHash-seaMonsterCount*monsterHash);
                }



            }


            
        }

        private bool IsCorner(Tile tile, List<Tile> tiles) => GetMatchingEdges(tile, tiles).Count == 2;

        private List<int> GetMatchingEdges(Tile tile, List<Tile> tiles)
        {
            var result = new HashSet<int>();

            foreach (var tile1 in tiles)
            {
                if (tile1.Id == tile.Id) continue;

                var r = GetMatchingEdge(tile, tile1);

                if (r >= 0) result.Add(r);
            }


            return result.ToList();
        }

        private int GetMatchingEdge(Tile tile1, Tile tile2)
        {
            var i = 0;
            foreach (var tile1Edge in tile1.Edges)
            {
                foreach (var tile2Edge in tile2.Edges)
                {
                    var reverse = tile2Edge.ToCharArray();
                    Array.Reverse(reverse);
                    if (tile1Edge == tile2Edge || tile1Edge == new string(reverse))
                        return i;
                }

                i++;
            }

            return -1;
        }

        private class TileFactory
        {
            private int _state;

            public TileFactory()
            {
                ImageData = new List<string>();
            }

            public int Id { get; set; }
            public List<string> ImageData { get; set; }

            private void Rotate90()
            {
                var result = ImageData.Select(i => "").ToList();


                foreach (var line in ImageData)
                    for (var i = line.Length - 1; i >= 0; i--)
                        result[line.Length - 1 - i] += line[i];

                ImageData = result;
            }

            private void FlipHorizontal() => ImageData.Reverse();

            public Tile GetNextState()
            {
                if (_state % 4 > 0) Rotate90();
                if (_state == 4) FlipHorizontal();

                var tile = new Tile {ImageData = ImageData, Id = Id};
                _state++;
                return tile;
            }
        }

        private class Tile
        {
            public int Id { get; set; }

            public List<string> ImageData { get; set; } = new ();

            public string TopBorder => ImageData.First();
            public string BottomBorder => ImageData.Last();
            public string LeftBorder => string.Join("", ImageData.Select(r => r.First()));
            public string RightBorder => string.Join("", ImageData.Select(r => r.Last()));

            public List<string> Edges => new () {TopBorder, BottomBorder, LeftBorder, RightBorder};
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day24 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day24));
            //var input = ReadTestInput(nameof(Day24));

            var directions = new List<List<Dir>>();
            foreach (var s in input)
            {
                var c = 0;
                var currLine = new List<Dir>();
                while (c < s.Length)
                {
                    if (s[c] == 'e')
                    {
                        currLine.Add(Dir.East);
                        c++;
                        continue;
                    }

                    if (s[c] == 'w')
                    {
                        currLine.Add(Dir.West);
                        c++;
                        continue;
                    }

                    if (s[c] == 'n')
                    {
                        if (s[c + 1] == 'e')
                        {
                            currLine.Add(Dir.NorthEast);
                            c += 2;
                            continue;
                        }

                        if (s[c + 1] == 'w')
                        {
                            currLine.Add(Dir.NorthWest);
                            c += 2;
                            continue;
                        }
                    }

                    if (s[c] == 's')
                    {
                        if (s[c + 1] == 'e')
                        {
                            currLine.Add(Dir.SouthEast);
                            c += 2;
                            continue;
                        }

                        if (s[c + 1] == 'w')
                        {
                            currLine.Add(Dir.SouthWest);
                            c += 2;
                        }
                    }
                }

                directions.Add(currLine);
            }

            var dictionary = new Dictionary<(int, int, int), TileColor>();

            foreach (var line in directions)
            {
                (int x,int y, int z) currPos = (0, 0, 0);
                foreach (var dir in line)
                {
                    switch (dir)
                    {
                        case Dir.East:
                            currPos = (currPos.x + 1, currPos.y - 1, currPos.z);
                            break;
                        case Dir.SouthEast:
                            currPos = (currPos.x, currPos.y-1, currPos.z +1);
                            break;
                        case Dir.SouthWest:
                            currPos = (currPos.x - 1, currPos.y, currPos.z+1);
                            break;
                        case Dir.West:
                            currPos = (currPos.x - 1, currPos.y+1, currPos.z);
                            break;
                        case Dir.NorthWest:
                            currPos = (currPos.x, currPos.y+1, currPos.z-1);
                            break;
                        case Dir.NorthEast:
                            currPos = (currPos.x + 1, currPos.y, currPos.z-1);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                if (!dictionary.TryAdd(currPos, TileColor.Black))
                {
                    if (dictionary[currPos] == TileColor.Black)
                    {
                        dictionary[currPos] = TileColor.White;    
                    }
                    else
                    {
                        dictionary[currPos] = TileColor.Black;
                    }
                }
            }


            Console.WriteLine(dictionary.Where(d=>d.Value == TileColor.Black).Count());
        }


        public void RunPartTwo()
        { 
            var input = ReadInput(nameof(Day24));
            //var input = ReadTestInput(nameof(Day24));

            var directions = new List<List<Dir>>();
            foreach (var s in input)
            {
                var c = 0;
                var currLine = new List<Dir>();
                while (c < s.Length)
                {
                    if (s[c] == 'e')
                    {
                        currLine.Add(Dir.East);
                        c++;
                        continue;
                    }

                    if (s[c] == 'w')
                    {
                        currLine.Add(Dir.West);
                        c++;
                        continue;
                    }

                    if (s[c] == 'n')
                    {
                        if (s[c + 1] == 'e')
                        {
                            currLine.Add(Dir.NorthEast);
                            c += 2;
                            continue;
                        }

                        if (s[c + 1] == 'w')
                        {
                            currLine.Add(Dir.NorthWest);
                            c += 2;
                            continue;
                        }
                    }

                    if (s[c] == 's')
                    {
                        if (s[c + 1] == 'e')
                        {
                            currLine.Add(Dir.SouthEast);
                            c += 2;
                            continue;
                        }

                        if (s[c + 1] == 'w')
                        {
                            currLine.Add(Dir.SouthWest);
                            c += 2;
                        }
                    }
                }

                directions.Add(currLine);
            }

            var dictionary = new Dictionary<(int x, int y, int z), TileColor>();

            foreach (var line in directions)
            {
                (int x,int y, int z) currPos = (0, 0, 0);
                foreach (var dir in line)
                {
                    currPos = dir switch
                    {
                        Dir.East => (currPos.x + 1, currPos.y - 1, currPos.z),
                        Dir.SouthEast => (currPos.x, currPos.y - 1, currPos.z + 1),
                        Dir.SouthWest => (currPos.x - 1, currPos.y, currPos.z + 1),
                        Dir.West => (currPos.x - 1, currPos.y + 1, currPos.z),
                        Dir.NorthWest => (currPos.x, currPos.y + 1, currPos.z - 1),
                        Dir.NorthEast => (currPos.x + 1, currPos.y, currPos.z - 1),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                if (!dictionary.TryAdd(currPos, TileColor.Black))
                {
                    if (dictionary[currPos] == TileColor.Black)
                    {
                        dictionary[currPos] = TileColor.White;    
                    }
                    else
                    {
                        dictionary[currPos] = TileColor.Black;
                    }
                }
            }

            

            var i = 0;
            while (i < 100)
            {
                var extraToConsider = new Dictionary<(int x, int y, int z), TileColor>();
                foreach (var tile in dictionary)
                {
                    var neighbours = new List<((int x, int y, int z) coordinates, TileColor color)>();
                    var allDirections = Enum.GetValues<Dir>();
                    foreach (var dir in allDirections)
                    {
                        var neighbour = dir switch
                        {
                            Dir.East => (tile.Key.x + 1, tile.Key.y - 1, tile.Key.z),
                            Dir.SouthEast => (tile.Key.x, tile.Key.y - 1, tile.Key.z + 1),
                            Dir.SouthWest => (tile.Key.x - 1, tile.Key.y, tile.Key.z + 1),
                            Dir.West => (tile.Key.x - 1, tile.Key.y + 1, tile.Key.z),
                            Dir.NorthWest => (tile.Key.x, tile.Key.y + 1, tile.Key.z - 1),
                            Dir.NorthEast => (tile.Key.x + 1, tile.Key.y, tile.Key.z - 1),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        neighbours.Add(dictionary.TryGetValue(neighbour, out var t)
                            ? (neighbour, t)
                            : (neighbour, TileColor.White));
                    }

                    foreach (var neighbour in neighbours)
                    {
                        extraToConsider.TryAdd(neighbour.coordinates, neighbour.color);
                    }
                }

                foreach (var extra in extraToConsider)
                {
                    dictionary.TryAdd(extra.Key, extra.Value);
                }
                
                var tempDictionary = new Dictionary<(int x, int y, int z), TileColor>(dictionary);
                foreach (var tile in dictionary)
                {
                    var neighbours = new List<((int x, int y, int z) coordinates, TileColor color)>();
                    var allDirections = Enum.GetValues<Dir>();
                    foreach (var dir in allDirections)
                    {
                        var neighbour = dir switch
                        {
                            Dir.East => (tile.Key.x + 1, tile.Key.y - 1, tile.Key.z),
                            Dir.SouthEast => (tile.Key.x, tile.Key.y - 1, tile.Key.z + 1),
                            Dir.SouthWest => (tile.Key.x - 1, tile.Key.y, tile.Key.z + 1),
                            Dir.West => (tile.Key.x - 1, tile.Key.y + 1, tile.Key.z),
                            Dir.NorthWest => (tile.Key.x, tile.Key.y + 1, tile.Key.z - 1),
                            Dir.NorthEast => (tile.Key.x + 1, tile.Key.y, tile.Key.z - 1),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        neighbours.Add(dictionary.TryGetValue(neighbour, out var t)
                            ? (neighbour, t)
                            : (neighbour, TileColor.White));
                    }
                    var blackNeighbours = neighbours.Where(n => n.color == TileColor.Black).Count();

                    if(tile.Value == TileColor.Black)
                    {
                        if (blackNeighbours == 0 || blackNeighbours > 2)
                        {
                            tempDictionary[tile.Key] = TileColor.White;
                        }
                    }
                    else
                    {
                        if (blackNeighbours == 2)
                        {
                            tempDictionary[tile.Key] = TileColor.Black;
                        }
                    }
                }

                dictionary = tempDictionary;
                i++;
                Console.WriteLine($"Day {i}: " + dictionary.Where(k=>k.Value == TileColor.Black).Count());
            }
            
        }
    }

    internal enum Dir
    {
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest,
        NorthEast
    }

    internal enum TileColor
    {
        White,
        Black
    }
}
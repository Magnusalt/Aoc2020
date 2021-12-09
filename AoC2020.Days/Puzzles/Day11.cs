using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day11 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day11));
            //var input = ReadTestInput(nameof(Day11));

            var resulting = new string[input.Length];
            var r = 0;
            foreach (var s in input)
            {
                resulting[r] = new string('.', input[0].Length);
                r++;
            }


            while (true)
            {
                for (var i = 0; i < input.Length; i++)
                for (var j = 0; j < input[0].Length; j++)
                {
                    var row = input[i];
                    var seat = row[j];
                    switch (seat)
                    {
                        case 'L':
                            var aroundEmpty = new List<bool>();
                            for (var y = i > 0 ? -1 : 0; y <= (i < input.Length - 1 ? 1 : 0); y++)
                            for (var x = j > 0 ? -1 : 0; x <= (j < input[0].Length - 1 ? 1 : 0); x++)
                            {
                                if (i + y == i && j + x == j) continue;

                                var isEmpty = input[i + y][j + x] == 'L' || input[i + y][j + x] == '.';
                                aroundEmpty.Add(isEmpty);
                            }

                            if (aroundEmpty.All(a => a)) resulting[i] = ReplaceAt(resulting[i], j, '#');

                            break;
                        case '#':
                            var aroundOcc = new List<bool>();
                            for (var y = i > 0 ? -1 : 0; y <= (i < input.Length - 1 ? 1 : 0); y++)
                            for (var x = j > 0 ? -1 : 0; x <= (j < input[0].Length - 1 ? 1 : 0); x++)
                            {
                                if (i + y == i && j + x == j) continue;

                                var notEmpty = input[i + y][j + x] == '#';
                                aroundOcc.Add(notEmpty);
                            }

                            if (aroundOcc.Count(a => a) >= 4) resulting[i] = ReplaceAt(resulting[i], j, 'L');

                            break;
                    }
                }

                if (SeatsStillChanges(input, resulting))
                {
                    var r1 = 0;
                    foreach (var s in input)
                    {
                        input[r1] = new string(resulting[r1]);
                        r1++;
                    }
                }
                else
                {
                    break;
                }
            }

            var count = resulting.Select(s => s.Count(c => c == '#')).Sum();


            Console.WriteLine(count);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day11));
            //var input = ReadTestInput(nameof(Day11));

            var resulting = new string[input.Length];
            var r = 0;
            foreach (var s in input)
            {
                resulting[r] = new string('.', input[0].Length);
                r++;
            }


            while (true)
            {
                for (var i = 0; i < input.Length; i++)
                for (var j = 0; j < input[0].Length; j++)
                {
                    var row = input[i];
                    var seat = row[j];
                    var dirs = new[] {(1, 1), (1, 0), (-1, 1), (0, 1), (-1, -1), (-1, 0), (1, -1), (0, -1)};

                    switch (seat)
                    {
                        case 'L':
                            var aroundEmpty = new List<bool>();
                            foreach (var dir in dirs)
                            {
                                var dist = 1;
                                do
                                {
                                    var dx = j + dir.Item1 * dist;
                                    var dy = i + dir.Item2 * dist;
                                    dist++;
                                    try
                                    {
                                        var c = input[dy][dx];
                                        if (c == '#')
                                        {
                                            aroundEmpty.Add(false);
                                            break;
                                        }

                                        if (c == 'L')
                                        {
                                            aroundEmpty.Add(true);
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        break;
                                    }
                                } while (true);
                            }

                            if (aroundEmpty.All(c=>c)) resulting[i] = ReplaceAt(resulting[i], j, '#');

                            break;
                        case '#':
                            var aroundOcc = new List<bool>();
                            foreach (var dir in dirs)
                            {
                                var dist = 1;
                                do
                                {
                                    var dx = j + dir.Item1 * dist;
                                    var dy = i + dir.Item2 * dist;
                                    dist++;
                                    try
                                    {
                                        var c = input[dy][dx];
                                        if (c == '#')
                                        {
                                            aroundOcc.Add(true);
                                            break;
                                        }

                                        if (c == 'L')
                                        {
                                            aroundOcc.Add(false);
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        break;
                                    }
                                } while (true);
                            }

                            if (aroundOcc.Count(a => a) > 4) resulting[i] = ReplaceAt(resulting[i], j, 'L');

                            break;
                    }
                }

                if (SeatsStillChanges(input, resulting))
                {
                    var r1 = 0;
                    foreach (var s in input)
                    {
                        input[r1] = new string(resulting[r1]);
                        r1++;
                    }
                }
                else
                {
                    break;
                }
            }

            var count = resulting.Select(s => s.Count(c => c == '#')).Sum();


            Console.WriteLine(count);
        }

        private bool SeatsStillChanges(string[] input, string[] curr)
        {
            for (var i = 0; i < input.Length; i++)
                if (input[i] != curr[i])
                    return true;

            return false;
        }

        public static string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null) throw new ArgumentNullException("input");
            var chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
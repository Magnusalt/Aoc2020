using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day10 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = ReadInput(nameof(Day10));
            var input = ReadInput(nameof(Day10)).Select(int.Parse).OrderBy(i => i).ToList();

            var device = input.Last() + 3;

            var currJ = 0;

            var dict = new Dictionary<int, int>();

            foreach (var i in input.Append(device))
            {
                var deltaOutput = i - currJ;
                if (dict.ContainsKey(deltaOutput))
                    dict[deltaOutput]++;
                else
                    dict.Add(deltaOutput, 1);
                currJ += deltaOutput;
            }


            Console.WriteLine(dict[1] * dict[3]);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day10)).Select(int.Parse).OrderBy(i => i).ToList();
            var device = input.Last() + 3;

            var consecutiveOnes = 0;
            var current = 0;
            long total = 1;

            foreach (var num in input.Append(device))
            {
                var diff = num - current;
                if (diff == 1)
                {
                    consecutiveOnes++;
                }
                if (diff == 3)
                {
                    switch (consecutiveOnes)
                    {
                        case 2:
                            total *= 2;
                            break;
                        case 3:
                            total *= 4;
                            break;
                        case 4:
                            total *= 7;
                            break;
                    }
                    consecutiveOnes = 0;
                }
                current = num;
            }

            Console.WriteLine(total);
        }
    }
}
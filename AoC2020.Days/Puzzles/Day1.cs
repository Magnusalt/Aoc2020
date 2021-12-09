using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day1 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day1));

            var parsed = input.Select(int.Parse).ToList();

            var n1 = 0;
            var n2 = 0;

            foreach (var i in parsed)
            {
                var found = parsed.Where(p => p + i == 2020);
                if (found.Any())
                {
                    n1 = i;
                    n2 = found.First();
                    break;
                }
            }


            Console.WriteLine(n1*n2);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day1));

            var parsed = input.Select(int.Parse).ToList();

            var n1 = 0;
            var n2 = 0;

            foreach (var i in parsed)
            {
                foreach (var i1 in parsed)
                {
                    foreach (var i2 in parsed)
                    {
                        if (i + i1 + i2 == 2020)
                        {
                            Console.WriteLine($"{i}, {i1}, {i2}");
                            Console.WriteLine(i*i1*i2);

                        }
                    }
                }
            }


            Console.WriteLine(n1 * n2);
        }
    }
}
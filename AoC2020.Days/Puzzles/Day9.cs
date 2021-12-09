using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day9 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = ReadTestInput(nameof(Day9)).Select(int.Parse).ToList();
            var input = ReadInput(nameof(Day9)).Select(long.Parse).ToList();

            var preambleLength = 25;
            long result = 0;
            for (var i = preambleLength; i < input.Count; i++)
            {
                var curr = input[i];

                var startSlice = i - preambleLength;
                var slice = input.Skip(startSlice).Take(preambleLength).ToArray();

                var isValid = false;

                for (var j = 0; j < preambleLength; j++)
                {
                    for (var k = 0; k < preambleLength; k++)
                    {
                        if (j == k) continue;

                        isValid = curr - slice[j] - slice[k] == 0;
                        if (isValid) break;
                    }

                    if (isValid) break;
                }

                if (isValid) continue;
                result = curr;
                break;
            }


            Console.WriteLine(result);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day9)).Select(long.Parse).ToList();

            var preambleLength = 25;
            long result = 0;
            for (var i = preambleLength; i < input.Count; i++)
            {
                var curr = input[i];

                var startSlice = i - preambleLength;
                var slice = input.Skip(startSlice).Take(preambleLength).ToArray();

                var isValid = false;

                for (var j = 0; j < preambleLength; j++)
                {
                    for (var k = 0; k < preambleLength; k++)
                    {
                        if (j == k) continue;

                        isValid = curr - slice[j] - slice[k] == 0;
                        if (isValid) break;
                    }

                    if (isValid) break;
                }

                if (isValid) continue;
                result = curr;
                break;
            }

            var foundSeq = false;
            var window = new List<long>();
            var windowSize = 2;

            while (!foundSeq)
            {
                var start = 0;

                while (start + windowSize < input.Count)
                {
                    window = input.Skip(start).Take(windowSize).ToList();
                    if (window.Sum() == result)
                    {
                        foundSeq = true;
                        break;
                    }

                    start++;
                }

                if (foundSeq)
                    break;
                windowSize++;
            }

            Console.WriteLine(window.Min() + window.Max());
        }
    }
}
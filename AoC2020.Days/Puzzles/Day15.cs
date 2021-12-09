using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day15 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = new []{19,20,14,0,9,1};
            var input = new[] {0, 3, 6};

            var index = 0;

            var numbers = new List<int>();

            while (index < 2020)
            {
                if (index < input.Length)
                {
                    numbers.Add(input[index]);
                }
                else
                {
                    var last = numbers.Last();
                    var lastCount = numbers.Count(l => l == last);
                    if (lastCount == 1)
                    {
                        numbers.Add(0);
                    }
                    else
                    {
                        var indexOfPrevOcc = numbers.LastIndexOf(last);

                        var nextBeforIndex =
                            numbers.SkipLast(numbers.Count - indexOfPrevOcc).ToList().LastIndexOf(last);
                        numbers.Add(indexOfPrevOcc - nextBeforIndex);
                    }
                }

                index++;
            }


            Console.WriteLine(numbers.Last());
        }

        public void RunPartTwo()
        {
            var input = new[] {19, 20, 14, 0, 9, 1};
            var index = 0;

            var numbers = new Dictionary<int, (int nextLast, int last)>();
            var lastAdded = 0;

            while (index < 30000000)
            {
                if (index < input.Length)
                {
                    numbers.Add(input[index], (index, index));
                    lastAdded = input[index];
                }
                else
                {
                    var (nextLast, last) = numbers[lastAdded];
                    var nextKey = last - nextLast;
                    if (numbers.ContainsKey(nextKey))
                    {
                        var t = numbers[nextKey];
                        numbers[nextKey] = (t.last, index);
                    }
                    else
                    {
                        numbers.Add(nextKey, (index, index));
                    }

                    lastAdded = nextKey;
                }

                index++;
            }

            Console.WriteLine(lastAdded);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day7 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day7));

            var dict = new Dictionary<string, string>();

            foreach (var s in input)
            {
                var split = s.Split("contain");

                var key = split[0].Split(' ');

                dict[$"{key[0]} {key[1]}"] = split[1].Trim();
            }

            var direct = dict.Where(d => d.Value.Contains("shiny gold"));
            var queue = new Queue<KeyValuePair<string, string>>();

            foreach (var keyValuePair in direct) queue.Enqueue(keyValuePair);

            var set = new HashSet<string>();


            while (queue.Count > 0)
            {
                var e = queue.Dequeue();
                set.Add(e.Key);

                var fromDict = dict.Where(d => d.Value.Contains(e.Key));

                foreach (var keyValuePair in fromDict) queue.Enqueue(keyValuePair);
            }


            Console.WriteLine(set.Count);
        }


        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day7));

            var dict = new Dictionary<string, string>();

            foreach (var s in input)
            {
                var split = s.Split("contain");

                var key = split[0].Split(' ');


                dict[$"{key[0]} {key[1]}"] = split[1].Trim();
            }


            var queue = new Queue<KeyValuePair<string, string>>();

            queue.Enqueue(dict.Single(kv => kv.Key == "shiny gold"));


            var count = 0;
            while (queue.Count > 0)
            {
                var e = queue.Dequeue();
                count++;
                var child = e.Value.Split(',').Where(s => !s.Contains("no other")).Select(s=>s.Trim());

                foreach (var c in child)
                {
                    var data = c.Split(' ');
                    var n = int.Parse(data[0]);
                    for (var i = 0; i < n; i++)
                    {
                        var childNode = dict.FirstOrDefault(kv => kv.Key.Contains($"{data[1]} {data[2]}"));

                        queue.Enqueue(childNode);
                    }
                }
            }

            Console.WriteLine(count);
            }
        }
    }
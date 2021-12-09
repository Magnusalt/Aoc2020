using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace AoC2020.Days.Puzzles
{
    public class Day13 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day13));
            //var input = ReadTestInput(nameof(Day13));

            var myTime = int.Parse(input[0]);
            var buses = input[1]
                .Split(',')
                .Where(c => c != "x")
                .Select(int.Parse)
                .ToDictionary(k => k, v => 0);

            var result = new Dictionary<int, long>();

            while (buses.Any(kv => kv.Value < myTime))
                foreach (var (key, value) in buses)
                {
                    buses[key] += key;
                    if (buses[key] > myTime && !result.ContainsKey(key)) result.Add(key, buses[key]);
                }

            var available = result.Where(kv => kv.Value > myTime).OrderBy(kv => kv.Value).First();

            Console.WriteLine((available.Value - myTime) * available.Key);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day13));
            //var input = ReadTestInput(nameof(Day13));

            var sb = new StringBuilder();
            var i = 0;
            foreach (var s in input[1].Split(','))
            {
                if (s == "x")
                {
                    i++;
                    continue;
                }

                var eq = $"t+{i}%{s}=0";
                i++;
                sb.Append(eq+",");
            }


            Console.WriteLine("www.wolframalpha.com/input?i="+UrlEncoder.Create().Encode(sb.ToString()));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day16 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = ReadInput(nameof(Day16));
            //var rules = new Dictionary<string, Rule>();

            //var index = 0;
            //while (!string.IsNullOrEmpty(input[index]))
            //{
            //    var r = input[index].Split(':');

            //    var ranges = r[1].Split("or");
            //    var range1 = ranges[0].Split('-');
            //    var range2 = ranges[1].Split('-');

            //    var rule = new Rule
            //    {
            //        LowerRange1 = int.Parse(range1[0]),
            //        UpperRange1 = int.Parse(range1[1]),
            //        LowerRange2 = int.Parse(range2[0]),
            //        UpperRange2 = int.Parse(range2[1])
            //    };

            //    rules.Add(r[0], rule);

            //    index++;
            //}

            //var tickets = new List<int[]>();

            //while (index < input.Length)
            //{
            //    if (input[index].Contains("ticket") || string.IsNullOrEmpty(input[index]))
            //    {
            //        index++;
            //        continue;
            //    }

            //    tickets.Add(input[index].Split(',').Select(int.Parse).ToArray());
            //    index++;
            //}

            //long errorRate = 0;

            //foreach (var ticket in tickets.Skip(1))
            //{
            //    foreach (var i in ticket)
            //    {
            //        if (!rules.Any(r=>r.Value.IsInRange(i)))
            //        {
            //            errorRate += i;
            //        }
            //    }
            //}

            //Console.WriteLine(errorRate);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day16));
            var rules = new Dictionary<string, Rule>();

            var index = 0;
            while (!string.IsNullOrEmpty(input[index]))
            {
                var r = input[index].Split(':');

                var ranges = r[1].Split("or");
                var range1 = ranges[0].Split('-');
                var range2 = ranges[1].Split('-');

                var rule = new Rule
                {
                    Name = r[0],
                    LowerRange1 = int.Parse(range1[0]),
                    UpperRange1 = int.Parse(range1[1]),
                    LowerRange2 = int.Parse(range2[0]),
                    UpperRange2 = int.Parse(range2[1])
                };

                rules.Add(r[0], rule);

                index++;
            }

            var tickets = new List<int[]>();

            while (index < input.Length)
            {
                if (input[index].Contains("ticket") || string.IsNullOrEmpty(input[index]))
                {
                    index++;
                    continue;
                }

                tickets.Add(input[index].Split(',').Select(int.Parse).ToArray());
                index++;
            }

            var invalidTickets = new List<int[]>();

            foreach (var ticket in tickets)
            foreach (var i in ticket)
                if (!rules.Any(r => r.Value.IsInRange(i)))
                    invalidTickets.Add(ticket);

            var validTickets = tickets.Except(invalidTickets).ToList();


            var myTicket = tickets[0];


            var potentials = new Dictionary<int, List<Rule>>();

            for (var i = 0; i < myTicket.Length; i++)
            {
                var col = validTickets.Select(t => t[i]).ToList();

                potentials.Add(i, new List<Rule>());

                foreach (var r in rules)
                    if (col.All(c => r.Value.IsInRange(c)))
                        potentials[i].Add(r.Value);
            }



            while (potentials.Any(p=>p.Value.Count > 1))
            {
                var known = potentials.Where(p => p.Value.Count == 1).ToList();

                foreach (var keyValuePair in potentials.Except(known))
                {
                    var left = keyValuePair.Value.Except(known.SelectMany(kv => kv.Value));
                    potentials[keyValuePair.Key] = left.ToList();
                }

            }

            long ticketProd = 1;
            var departureRules = potentials.Where(r => r.Value.Single().Name.StartsWith("departure"));
            foreach (var finalRule in departureRules)
                ticketProd *= myTicket[finalRule.Key];

            Console.WriteLine(ticketProd);
        }
    }

    public class Rule
    {
        public string Name { get; set; }
        public int LowerRange1 { get; set; }
        public int UpperRange1 { get; set; }
        public int LowerRange2 { get; set; }
        public int UpperRange2 { get; set; }

        public bool IsInRange(int val)
        {
            var res = val >= LowerRange1 && val <= UpperRange1 || val >= LowerRange2 && val <= UpperRange2;
            return res;
        }
    }
}
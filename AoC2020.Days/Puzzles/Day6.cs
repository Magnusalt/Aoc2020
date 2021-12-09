using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day6 : Day, IDay
    {
        public void RunPartOne()
        {
            var res = ReadInput(nameof(Day6)).Append("").Aggregate((0, ""), (acc, s) =>
            {
                if (s.Length == 0)
                {
                    acc.Item1 += acc.Item2.Distinct().Count();
                    acc.Item2 = "";
                }
                else
                {
                    acc.Item2 += s;
                }

                return acc;
            }).Item1;
            Console.WriteLine(res);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day6));

            var answers = new List<Answer>();

            var temp = new Answer();

            foreach (var ans in input)
                if (ans.Length == 0)
                {
                    answers.Add(temp);
                    temp = new Answer();
                }
                else
                {
                    temp.Raw += ans;
                    temp.AnswerPerMember.Add(ans);
                }

            answers.Add(temp);

            var result = answers.Select(answer => answer.AnswerPerMember.Select(a => a.ToList())
                    .Skip(1)
                    .Aggregate(new HashSet<char>(answer.AnswerPerMember.First().ToList()), (h, e) =>
                    {
                        h.IntersectWith(e);
                        return h;
                    }))
                .Select(r => r.Count)
                .Sum();


            Console.WriteLine(result);
        }

        private class Answer
        {
            public Answer()
            {
                AnswerPerMember = new List<string>();
            }

            public string Raw { get; set; }
            public string Unique => string.Join("", Raw.Distinct());
            public int Distinct => Raw.Distinct().Count();

            public List<string> AnswerPerMember { get; }
        }
    }
}
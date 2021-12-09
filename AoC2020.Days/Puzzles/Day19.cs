using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Days.Puzzles
{
    public class Day19 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadTestInput(nameof(Day19));

            var index = 0;

            var rules = new Dictionary<int, MatchRule>();

            while (input[index] != string.Empty)
            {
                var r = input[index];
                var split = r.Split(':');
                var ruleIndex = int.Parse(split[0]);


                if (r.Contains('"'))
                {
                    rules.Add(ruleIndex, new LetterRule(split[1][2]));
                    index++;
                    continue;
                }

                if (r.Contains('|'))
                {
                    var subRules = split[1].Split('|');
                    var rule1 = subRules[0].Split(' ').Where(c => !string.IsNullOrEmpty(c))
                        .Select(c => int.Parse(c.Trim())).ToArray();
                    var rule2 = subRules[1].Split(' ').Where(c => !string.IsNullOrEmpty(c))
                        .Select(c => int.Parse(c.Trim())).ToArray();
                    var srule1 = new SimpleRule(rule1);
                    var srule2 = new SimpleRule(rule2);
                    rules.Add(ruleIndex, new PipedRule(srule1, srule2));
                    index++;
                    continue;
                }

                var ruleList = split[1].Split(' ').Where(c => !string.IsNullOrEmpty(c)).Select(c => int.Parse(c.Trim()))
                    .ToArray();
                var simpleRule = new SimpleRule(ruleList);

                rules.Add(ruleIndex, simpleRule);
                index++;
            }

            index++;

            var matched = 0;
            while (index < input.Length)
            {
                var toTest = input[index];

                var wasMatch = rules[0].Match(rules, toTest);

                if (wasMatch.match && wasMatch.depth == toTest.Length) matched++;

                index++;
            }


            Console.WriteLine(matched);
        }


        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day19));
            var result = Solve(input, true);

            Console.WriteLine(result);

        }

        public static int Solve(string[] input, bool part2)
        {
            var (rules, messages) = Parse(input);

            if (part2)
            {
                rules[8] = "c";
                rules[11] = "d";
            }

            SimplifyRules(rules);

            var rule0 = rules[0];

            if (part2)
            {
                // Rule8  ==> (Rule42)+  [one or more]
                // Rule11 ==>  Rule42{k}Rule31{k} where k >= 1 [balanced group]
                rule0 = rule0
                    .Replace("c", $"(?:{rules[42]})+")
                    .Replace("d", $"(?<DEPTH>{rules[42]})+(?<-DEPTH>{rules[31]})+(?(DEPTH)(?!))");
            }

            var rule0Regex = new Regex($"^{rule0}$", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

            return messages.Count(rule0Regex.IsMatch);
        }


        private static void SimplifyRules(IDictionary<int, string> dict)
        {
            var done = new HashSet<int>();
            foreach (var (key, val) in dict)
                if (val.Length == 1)
                    done.Add(key);

            while (done.Count != dict.Count)
            {
                foreach (var (key, val) in dict)
                {
                    if (done.Contains(key))
                        continue;

                    var remain = false;
                    dict[key] = Regex.Replace(val, @"\d+", m =>
                    {
                        var mKey = int.Parse(m.Value);
                        if (done.Contains(mKey))
                            return $"(?:{dict[mKey]})";

                        remain = true;
                        return m.Value;
                    });

                    if (!remain)
                        done.Add(key);
                }
            }
        }

        private static (Dictionary<int, string>, List<string>) Parse(IEnumerable<string> input)
        {
            var dict = new Dictionary<int, string>();
            var messages = new List<string>();

            var mode = false;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    mode = true;
                }
                else if (mode)
                {
                    messages.Add(line);
                }
                else
                {
                    var split = line.Split(':');
                    dict[int.Parse(split[0])] = split[1] switch
                    {
                        var s when s[1] is '"' => s[2..^1],
                        var s => s[1..]
                    };
                }
            }

            return (dict, messages);
        }
    }

    internal abstract class MatchRule
    {
        public abstract (bool match, int depth) Match(Dictionary<int, MatchRule> rules, string toTest);
    }

    internal class PipedRule : MatchRule
    {
        private readonly SimpleRule _rule1;
        private readonly SimpleRule _rule2;

        public PipedRule(SimpleRule rule1, SimpleRule rule2)
        {
            _rule1 = rule1;
            _rule2 = rule2;
        }

        public override (bool, int) Match(Dictionary<int, MatchRule> rules, string toTest)
        {
            var res1 = _rule1.Match(rules, toTest);
            var res2 = _rule2.Match(rules, toTest);

            if (res1.Item1) return res1;
            if (res2.Item1) return res2;

            return (false, 0);
        }
    }

    internal class LetterRule : MatchRule
    {
        private readonly char _c;

        public LetterRule(char c)
        {
            _c = c;
        }

        public override (bool, int) Match(Dictionary<int, MatchRule> rules, string toTest)
        {
            if (string.IsNullOrEmpty(toTest))
            {
                return (true, 0);
            }
            return (toTest[0] == _c, toTest[0] == _c ? 1 : 0);
        }
    }

    internal class SimpleRule : MatchRule
    {
        public SimpleRule(int[] rules)
        {
            Rules = rules;
        }

        public int[] Rules { get; }
        
        public override (bool, int) Match(Dictionary<int, MatchRule> rules, string toTest)
        {
            var matchDepth = 0;
            foreach (var rule in Rules)
            {
                var r = rules[rule];
                var (match, depth) = r.Match(rules, toTest.Substring(matchDepth));
                if (match)
                    matchDepth += depth;
                else
                    return (false, 0);
            }

            return (true, matchDepth);
        }
    }
}
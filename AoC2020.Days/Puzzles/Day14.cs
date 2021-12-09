using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AoC2020.Days.Puzzles
{
    public class Day14 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day14));
            //var input = ReadTestInput(nameof(Day14));


            var memory = new Dictionary<int, BigInteger>();

            var i = 0;

            var mask = "";

            while (i < input.Length)
            {
                if (input[i].StartsWith("mask"))
                {
                    mask = input[i].Split(' ').Last();
                    i++;
                    continue;
                }

                var value = BigInteger.Parse(input[i].Split(' ').Last());

                for (var j = 0; j < mask.Length; j++)
                {
                    var b = mask[j];
                    if (b == '0')
                    {
                        var bitMask0 = ~((BigInteger) 1 << (35 - j));
                        value &= bitMask0;
                    }

                    if (b == '1')
                    {
                        var bitMask1 = (BigInteger) 1 << (35 - j);
                        value |= bitMask1;
                    }
                }

                var address =
                    int.Parse(new string(input[i].SkipWhile(c => c != '[').TakeWhile(c => c != ']').Skip(1).ToArray()));

                if (!memory.ContainsKey(address))
                    memory.Add(address, value);
                else
                    memory[address] = value;

                i++;
            }

            BigInteger sum = 0;
            foreach (var m in memory) sum += m.Value;

            Console.WriteLine(sum);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day14));
            //var input = ReadTestInput(nameof(Day14));


            var memory = new Dictionary<BigInteger, BigInteger>();

            var i = 0;

            var mask = "";

            while (i < input.Length)
            {
                if (input[i].StartsWith("mask"))
                {
                    mask = input[i].Split(' ').Last();
                    i++;
                    continue;
                }

                var value = BigInteger.Parse(input[i].Split(' ').Last());

                var address =
                    BigInteger.Parse(new string(input[i].SkipWhile(c => c != '[').TakeWhile(c => c != ']').Skip(1)
                        .ToArray()));

                var floating = new List<int>();

                var resultingAddress = new List<BigInteger>();

                for (var j = 0; j < mask.Length; j++)
                {
                    var b = mask[j];
                    if (b == '0')
                    {
                        //var bitMask0 = ~((BigInteger) 1 << (35 - j));
                        //address &= bitMask0;
                    }

                    if (b == '1')
                    {
                        var bitMask1 = (BigInteger) 1 << (35 - j);
                        address |= bitMask1;
                    }

                    if (b == 'X') floating.Add(j);
                }

                

                var perm = GetPermutationsWithRept(new[] {0, 1}, floating.Count);

                foreach (var p in perm)
                {
                    var baseAddress = address;

                    foreach (var (first, second) in p.Zip(floating))
                    {
                        if (first == 0)
                        {
                            var bitMask0 = ~((BigInteger)1 << (35 - second));
                            baseAddress &= bitMask0;
                        }

                        if (first == 1)
                        {
                            var bitMask1 = (BigInteger)1 << (35 - second);
                            baseAddress |= bitMask1;
                        }
                    }
                    resultingAddress.Add(baseAddress);
                }

                foreach (var bigInteger in resultingAddress)
                    if (!memory.ContainsKey(bigInteger))
                        memory.Add(bigInteger, value);
                    else
                        memory[bigInteger] = value;


                i++;
            }

            BigInteger sum = 0;
            foreach (var m in memory) sum += m.Value;

            Console.WriteLine(sum);
        }

        private static IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] {t});
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new[] {t2}));
        }
    }
}
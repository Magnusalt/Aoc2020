using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day8 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day8));

            var acc = 0;
            var pc = 0;

            var seen = new HashSet<int>();


            while (true)
            {
                seen.Add(pc);
                var curr = input[pc].Split(' ');
                var ins = curr[0];
                var sign = curr[1][0] == '+' ? 1 : -1;
                var val = int.Parse(curr[1].Substring(1)) * sign;
                
                

                switch (ins)
                {
                    case "acc":
                        acc += val;
                        pc++;
                        break;
                    case "jmp":
                        pc += val;
                        break;
                    case "nop":
                        pc++;
                        break;

                }
                if (seen.Contains(pc))
                {
                    break;
                }

            }


            System.Console.WriteLine(acc);
        }



        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day8));

            var acc = 0;
            var pc = 0;
            var flipIndex = 0;
            while (pc != input.Length)
            {
                acc = 0;
                pc = 0;
                var prgmCopy = new List<string>(input);

                while (prgmCopy[flipIndex].StartsWith("acc"))
                {
                    flipIndex++;
                }

                prgmCopy[flipIndex] = prgmCopy[flipIndex].StartsWith("nop")
                    ? prgmCopy[flipIndex].Replace("nop", "jmp")
                    : prgmCopy[flipIndex].Replace("jmp", "nop");

                flipIndex++;

                var seen = new HashSet<int>();

                while (pc != prgmCopy.Count)
                {
                    seen.Add(pc);
                    var curr = prgmCopy[pc].Split(' ');
                    var ins = curr[0];
                    var sign = curr[1][0] == '+' ? 1 : -1;
                    var val = int.Parse(curr[1].Substring(1)) * sign;

                    switch (ins)
                    {
                        case "acc":
                            acc += val;
                            pc++;
                            break;
                        case "jmp":
                            pc += val;
                            break;
                        case "nop":
                            pc++;
                            break;

                    }

                    if (seen.Contains(pc))
                    {
                        break;
                    }

                }

            }


            System.Console.WriteLine(acc);
        }
    }
}
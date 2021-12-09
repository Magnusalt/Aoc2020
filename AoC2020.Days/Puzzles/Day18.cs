using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AoC2020.Days.Puzzles
{
    public class Day18 : Day, IDay
    {
        public void RunPartOne()
        {
            //var inputs = new string[]
            //{
            //    "1 + 2 * 3 + 4 * 5 + 6"

            //};
            var inputs = ReadInput(nameof(Day18));


            long result = 0;
            foreach (var input in inputs)
            { 
                var stacks = new Stack<Stack<string>>();

                stacks.Push(new Stack<string>());

                var index = 0;

                while (index < input.Length)
                {
                    if (input[index] == '(')
                    {
                        stacks.Push(new Stack<string>());
                        index++;
                        continue;
                    }

                    if (char.IsDigit(input[index]))
                    {
                        var s = "";
                        while (index < input.Length && input[index] != ' ' && input[index] != ')')
                        {
                            s += input[index++];
                        }

                        stacks.Peek().Push(s);
                        continue;
                    }

                    if (input[index] == ')')
                    {
                        var inner = stacks.Pop();
                        var innerResult = CalculateStack(inner);
                        if (stacks.Count > 0)
                            stacks.Peek().Push(innerResult.ToString());
                        index++;
                        continue;
                    }

                    stacks.Peek().Push(input[index].ToString());
                    index++;
                }

                result += CalculateStack(stacks.Pop());

            }

            System.Console.WriteLine(result);
        }


        private long CalculateStack(Stack<string> inner)
        {
            var reversed = new Stack<string>();
            while (inner.Count > 0)
            {
                reversed.Push(inner.Pop());
            }

            var first = reversed.Pop();
            long innerResult = long.Parse(first);
            while (reversed.Count > 0)
            {
                var curr = reversed.Pop();
                if (curr == " ")
                {
                    continue;
                }

                if (curr == "*")
                {
                    reversed.Pop();
                    innerResult *= long.Parse(reversed.Pop());
                }

                if (curr == "+")
                {
                    reversed.Pop();
                    innerResult += long.Parse(reversed.Pop());
                }
            }

            return innerResult;
        }

        public void RunPartTwo()
        {
            //var inputs = new string[]
            //{
            //    "1 + (2 * 3) + (4 * (5 + 6))"

            //};
            var inputs = ReadInput(nameof(Day18));


            long result = 0;
            foreach (var input in inputs)
            {
                var stacks = new Stack<Stack<string>>();

                stacks.Push(new Stack<string>());

                var index = 0;

                while (index < input.Length)
                {
                    if (input[index] == '(')
                    {
                        stacks.Push(new Stack<string>());
                        index++;
                        continue;
                    }

                    if (char.IsDigit(input[index]))
                    {
                        var s = "";
                        while (index < input.Length && input[index] != ' ' && input[index] != ')')
                        {
                            s += input[index++];
                        }

                        stacks.Peek().Push(s);
                        continue;
                    }

                    if (input[index] == ')')
                    {
                        var inner = stacks.Pop();
                        var innerResult = CalculateAdvancedStack(inner);
                        if (stacks.Count > 0)
                            stacks.Peek().Push(innerResult.ToString());
                        index++;
                        continue;
                    }

                    stacks.Peek().Push(input[index].ToString());
                    index++;
                }

                result += CalculateAdvancedStack(stacks.Pop());

            }

            System.Console.WriteLine(result);
        }
        private long CalculateAdvancedStack(Stack<string> inner)
        {
            var reversed ="";
            while (inner.Count > 0)
            {
                reversed += inner.Pop();
            }

            var split = reversed.Split('*');

            var factors = new List<long>();

            foreach (var s in split)
            {
                var nums = s.Split('+').Select(n=>long.Parse(n.Trim())).Sum();
                factors.Add(nums);
            }

            var innerResult= 1L;
            foreach (var f in factors)
            {
                innerResult *= f;
            }

            
            return innerResult;
        }
    }
}
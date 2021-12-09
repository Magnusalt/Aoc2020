using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day23 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = "974618352";
            var rounds = 100;
            var nums = input.ToCharArray().Select(n => (int) n - (int) '0').ToList();
            var highestID = nums.Max();

            //if (max > 0)
            //    nums.AddRange(Enumerable.Range(highestID + 1, max - nums.Count));

            var index = new Node[10];
            var start = new Node(nums.First());
            index[nums.First()] = start;
            var prev = start;
            foreach (var v in nums.Skip(1))
            {
                var n = new Node(v);
                index[v] = n;
                prev.Next = n;
                //n.Prev = prev;
                prev = n;

                if (v > highestID)
                    highestID = v;
            }

            prev.Next = start;

            var curr = start;
            for (var j = 0; j < rounds; j++)
            {
                // Remove the three times after curr from the list
                var cut = curr.Next;
                curr.Next = cut.Next.Next.Next; // 100% fine and not ugly code...

                // Find the val where we want to insert the cut nodes
                var destVal = findDestination(curr.Val, cut, highestID);

                var ip = index[destVal];
                var ipn = ip.Next;
                var tail = cut.Next.Next;
                tail.Next = ipn;
                ip.Next = cut;

                curr = curr.Next;
            }


            Console.WriteLine($"P1: {ListToString(start, 1)}");
        }

        public void RunPartTwo()
        {
            var input = "974618352";
            var rounds = 10_000_000;
            var nums = input.ToCharArray().Select(n => (int)n - (int)'0').ToList();
            var highestID = nums.Max();
            var max = 1_000_000;
            nums.AddRange(Enumerable.Range(highestID + 1, max - nums.Count));

            var index = new Node[max+1];
            var start = new Node(nums.First());
            index[nums.First()] = start;
            var prev = start;
            foreach (var v in nums.Skip(1))
            {
                var n = new Node(v);
                index[v] = n;
                prev.Next = n;
                //n.Prev = prev;
                prev = n;

                if (v > highestID)
                    highestID = v;
            }

            prev.Next = start;

            var curr = start;
            for (var j = 0; j < rounds; j++)
            {
                // Remove the three times after curr from the list
                var cut = curr.Next;
                curr.Next = cut.Next.Next.Next; // 100% fine and not ugly code...

                // Find the val where we want to insert the cut nodes
                var destVal = findDestination(curr.Val, cut, highestID);

                var ip = index[destVal];
                var ipn = ip.Next;
                var tail = cut.Next.Next;
                tail.Next = ipn;
                ip.Next = cut;

                curr = curr.Next;
            }


            var node = index[1];
            var res = (ulong)node.Next.Val * (ulong)node.Next.Next.Val;
            Console.WriteLine($"P2: {res}");
        }


        private int findDestination(int start, Node cut, int highestID)
        {
            var dest = start == 1 ? highestID : start - 1;
            var a = cut.Val;
            var b = cut.Next.Val;
            var c = cut.Next.Next.Val;

            while (dest == a || dest == b || dest == c)
            {
                --dest;
                if (dest <= 0)
                    dest = highestID;
            }

            return dest;
        }

        private string ListToString(Node node, int startVal)
        {
            while (node.Val != startVal)
                node = node.Next;
            node = node.Next;

            var str = "";
            do
            {
                str += node.Val.ToString();
                node = node.Next;
            } while (node.Val != startVal);

            return str;
        }

        internal class Node
        {
            public Node(int v)
            {
                Val = v;
            }

            public int Val { get; set; }
            public Node Next { get; set; }
        }
    }
}
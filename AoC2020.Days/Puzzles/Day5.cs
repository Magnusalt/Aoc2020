using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day5 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day5));

            var resulting = new List<BoardingPass>();

            foreach (var bp in input)
            {
                var currBp = new BoardingPass();
                var upperRow = 127;
                var lowerRow = 0;
                var upperCol = 7;
                var lowerCol = 0;
                foreach (var c in bp)
                {
                    switch (c)
                    {
                        case 'F':
                            upperRow -= (upperRow + 1 - lowerRow) / 2;
                            break;
                        case 'B':
                            lowerRow += (upperRow + 1 - lowerRow) / 2;
                            break;
                        case 'R':
                            lowerCol += (upperCol + 1 - lowerCol) / 2;
                            break;
                        case 'L':
                            upperCol -= (upperCol + 1 - lowerCol) / 2;
                            break;
                    }
                    resulting.Add(currBp);
                }


                currBp.Row = bp[6] == 'F' ? lowerRow : upperRow;
                currBp.Col = bp.Last() == 'R' ? upperCol : lowerCol;
            }

            Console.WriteLine(resulting.Max(r=>r.Id));
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day5));

            var resulting = new List<BoardingPass>();

            foreach (var bp in input)
            {
                var currBp = new BoardingPass();
                var upperRow = 127;
                var lowerRow = 0;
                var upperCol = 7;
                var lowerCol = 0;
                foreach (var c in bp)
                {
                    switch (c)
                    {
                        case 'F':
                            upperRow -= (upperRow + 1 - lowerRow) / 2;
                            break;
                        case 'B':
                            lowerRow += (upperRow + 1 - lowerRow) / 2;
                            break;
                        case 'R':
                            lowerCol += (upperCol + 1 - lowerCol) / 2;
                            break;
                        case 'L':
                            upperCol -= (upperCol + 1 - lowerCol) / 2;
                            break;
                    }
                }


                currBp.Row = bp[6] == 'F' ? lowerRow : upperRow;
                currBp.Col = bp.Last() == 'R' ? upperCol : lowerCol;
                if (currBp.Row > 0 && currBp.Row < 127)
                {
                    resulting.Add(currBp);
                }
            }

            var sorted = resulting.OrderBy(bp => bp.Id).ToList();

            var mySeat = new BoardingPass();
            for (int i = 0; i < sorted.Count-1; i++)
            {
                var curr = sorted[i];
                var next = sorted[i+1];
                if (next.Id - curr.Id == 2)
                {
                    if (next.Row == curr.Row)
                    {
                        mySeat.Row = next.Row;
                    }
                    else if(next.Row > curr.Row)
                    {
                        if (curr.Col < 7)
                        {
                            mySeat.Row = curr.Row;
                            mySeat.Col = curr.Col + 1;
                        }
                        else
                        {
                            mySeat.Row = next.Row;
                            mySeat.Col = next.Col - 1;
                        }
                    }
                }
            }

            Console.WriteLine(mySeat.Id);
        }

        private class BoardingPass
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int Id => Row * 8 + Col;
        }
    }
}
using System.Collections.Generic;

namespace AoC2020.Days.Puzzles
{
    public class Day3 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day3));

            var height = input.Length;


            var colNbr = input[0].Length;

            var col = colNbr;

            var nbrOfRepeat = 1;

            while (col < height*3 )
            {
                col += colNbr;
                nbrOfRepeat++;
            }

            var arr = new bool[col, height];

            var rowNbr = 0;
            foreach (var row in input)
            {
                var x = 0;
                for (int i = 0; i < nbrOfRepeat; i++)
                {
                    foreach (var c in row)
                    {
                        arr[x, rowNbr] = c == '#';
                        x++;
                    }
                }

                rowNbr++;
            }

            var currX = 0;
            var currY = 0;
            var foundTrees = 0;

            while (currY < height-1)
            {
                currY++;
                currX += 3;
                var foundTree = arr[currX, currY];

                if (foundTree)
                {
                    foundTrees++;
                }
            }




            System.Console.WriteLine(foundTrees);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day3));

            var height = input.Length;


            var colNbr = input[0].Length;

            var col = colNbr;

            var nbrOfRepeat = 1;

            while (col < height * 7)
            {
                col += colNbr;
                nbrOfRepeat++;
            }

            var arr = new bool[col, height];

            var rowNbr = 0;
            foreach (var row in input)
            {
                var x = 0;
                for (int i = 0; i < nbrOfRepeat; i++)
                {
                    foreach (var c in row)
                    {
                        arr[x, rowNbr] = c == '#';
                        x++;
                    }
                }

                rowNbr++;
            }


            var slopes = new List<(int,int)>
            {
                (1,1),
                (3,1),
                (5,1),
                (7,1),
                (2,1)
            };
            long result = 1;
            foreach (var slope in slopes)
            {
                var currX = 0;
                var currY = 0;
                long foundTrees = 0;

                while (currY < height - 1)
                {
                    currY += slope.Item2;
                    currX += slope.Item1;
                    var foundTree = arr[currX, currY];

                    if (foundTree)
                    {
                        foundTrees++;
                    }
                }

                result *= foundTrees;
            }
            

           


            System.Console.WriteLine(result);
        }
    }
}
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day2 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day2));
            var result = 0;
            foreach (var password in input)
            {
                var polPas =password.Split(":");

                var pol = polPas[0];
                var pass = polPas[1].Trim();

                var polCountLetter = pol.Split(' ');
                var polLetter = polCountLetter[1].First();
                var polRange = polCountLetter[0].Split('-');
                var polLower = int.Parse(polRange[0]);
                var polUpper = int.Parse(polRange[1]);

                var passGrouped = pass.GroupBy(c => c);

                if (passGrouped.Any(pg=>pg.Key == polLetter))
                {
                    var group = passGrouped.Single(pg => pg.Key == polLetter);
                    if (group.Count() >= polLower && group.Count() <= polUpper)
                    {
                        result++;
                    }
                }

            }


            
            System.Console.WriteLine($"Day2 part 1:    {result}");
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day2));
            var result = 0;
            foreach (var password in input)
            {
                var polPas = password.Split(":");

                var pol = polPas[0];
                var pass = polPas[1].Trim();

                var polCountLetter = pol.Split(' ');
                var polLetter = polCountLetter[1].First();
                var polRange = polCountLetter[0].Split('-');
                var polLower = int.Parse(polRange[0]) -1 ;
                var polUpper = int.Parse(polRange[1]) - 1;

                var c1 = pass[polLower] == polLetter;
                if (polUpper < pass.Length)
                {
                    var c2 = pass[polUpper] == polLetter;
                    if (c1 ^ c2)
                    {
                        result++;
                    }
                }
                else
                {
                    if (c1)
                    {
                        result++;
                    }
                }
                

                
                

            }



            System.Console.WriteLine($"Day2 part 2:    {result}");
        }
    }
}
using System.IO;

namespace AoC2020.Days
{
    public abstract class Day
    {
        protected string[] ReadInput(string day)
        {
            return File.ReadAllLines($"Input/{day}.txt");
        }

        protected string[] ReadTestInput(string day)
        {
            return File.ReadAllLines($"Input/Test{day}.txt");
        }
    }
}
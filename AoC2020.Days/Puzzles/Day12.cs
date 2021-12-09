using System;
using System.Diagnostics.CodeAnalysis;

namespace AoC2020.Days.Puzzles
{
    public class Day12 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = ReadTestInput(nameof(Day12));
            var input = ReadInput(nameof(Day12));

            (long X, long Y) start = (0, 0);

            var facing = Math.PI / 2;

            foreach (var s in input)
            {
                var dir = s[0];
                var dist = int.Parse(s.Substring(1));
                var angle = Math.PI * dist / 180.0;

                switch (dir)
                {
                    case 'N':
                        start.Y += dist;
                        break;
                    case 'S':
                        start.Y -= dist;
                        break;
                    case 'E':
                        start.X += dist;
                        break;
                    case 'W':
                        start.X -= dist;
                        break;
                    case 'R':
                        facing += angle;
                        break;
                    case 'L':
                        facing -= angle;
                        break;
                    case 'F':
                        start.X += (int) (Math.Sin(facing) * dist);
                        start.Y += (int) (Math.Cos(facing) * dist);
                        break;
                }
            }


            Console.WriteLine(Math.Abs(start.X) + Math.Abs(start.Y));
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day12));
            //var input = ReadTestInput(nameof(Day12));

            (long X, long Y) ship = (0, 0);

            (long X, long Y) waypoint = (10, 1);


            foreach (var s in input)
            {
                var dir = s[0];
                var dist = int.Parse(s.Substring(1));
                var angle = Math.PI * dist / 180.0;
                var wpAngle = Math.Atan2(waypoint.X, waypoint.Y);

                switch (dir)
                {
                    case 'N':
                        waypoint.Y += dist;
                        break;
                    case 'S':
                        waypoint.Y -= dist;
                        break;
                    case 'E':
                        waypoint.X += dist;
                        break;
                    case 'W':
                        waypoint.X -= dist;
                        break;
                    case 'R':
                        var dXr = waypoint.X - ship.X;
                        var dYr = waypoint.Y - ship.Y;
                        switch (dist)
                        {
                            case 90:
                                waypoint.X = ship.X +  dYr;
                                waypoint.Y = ship.Y - dXr;
                                break;
                            case 180:
                                waypoint.X = ship.X - dXr;
                                waypoint.Y = ship.Y - dYr;
                                break;
                            case 270:
                                waypoint.X = ship.X - dYr;
                                waypoint.Y = ship.Y + dXr;
                                break;
                        }

                        break;
                    case 'L':
                        var dXl = waypoint.X - ship.X;
                        var dYl = waypoint.Y - ship.Y;
                        switch (dist)
                        {
                            case 90:
                                waypoint.X = ship.X - dYl;
                                waypoint.Y = ship.Y + dXl;
                                break;
                            case 180:
                                waypoint.X = ship.X - dXl;
                                waypoint.Y = ship.Y - dYl;
                                break;
                            case 270:
                                waypoint.X = ship.X + dYl;
                                waypoint.Y = ship.Y - dXl;
                                break;
                        }
                        break;
                    case 'F':
                        var dX = (waypoint.X - ship.X) * dist;
                        var dY = (waypoint.Y - ship.Y) * dist;
                        ship.X += dX;
                        ship.Y += dY;
                        waypoint.X += dX;
                        waypoint.Y += dY;
                        break;
                }
            }


            Console.WriteLine(Math.Abs(ship.X) + Math.Abs(ship.Y));
        }
    }
}

//        double angle    = Math.PI * degrees / 180.0;
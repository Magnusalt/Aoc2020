using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day17 : Day, IDay
    {
        public void RunPartOne()
        {
            //var input = ReadTestInput(nameof(Day17));
            var input = ReadInput(nameof(Day17));
            var cycles = 6;
            var state = new List<List<List<bool>>>();
            var nextState = new List<List<List<bool>>>();
            var sx = input[0].Length;
            var sy = input.Length;

            var f = 3;

            for (var z = 0; z < cycles * f + 1; z++)
            {
                state.Add(new List<List<bool>>());
                nextState.Add(new List<List<bool>>());

                for (var y = 0; y < sy + f * cycles; y++)
                {
                    state[z].Add(new List<bool>());
                    nextState[z].Add(new List<bool>());
                    for (var x = 0; x < sx + f * cycles; x++)
                        if (z == cycles && y >= cycles && y < cycles + sy && x >= cycles && x < cycles + sx)
                        {
                            state[z][y].Add(input[y - cycles][x - cycles] == '#');
                            nextState[z][y].Add(false);
                        }
                        else
                        {
                            state[z][y].Add(false);
                            nextState[z][y].Add(false);
                        }
                }
            }

            var cycleN = 0;
            while (cycleN < cycles)
            {
                for (var z = 1; z < cycles * f; z++)
                for (var y = 1; y < sy + f * cycles - 1; y++)
                for (var x = 1; x < sx + f * cycles - 1; x++)
                {
                    var b = state[z][y][x];
                    var activeNeigh = 0;
                    for (var dz = -1; dz <= 1; dz++)
                    for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        var neigh = state[z + dz][y + dy][x + dx];
                        if (neigh && (dz != 0 || dy != 0 || dx != 0)) activeNeigh++;
                    }

                    if (b)
                    {
                        if (activeNeigh == 2 || activeNeigh == 3)
                            nextState[z][y][x] = true;
                        else
                            nextState[z][y][x] = false;
                    }
                    else
                    {
                        if (activeNeigh == 3) nextState[z][y][x] = true;
                    }
                }

                for (var z = 0; z < cycles * f; z++)
                for (var y = 0; y < sy + f * cycles; y++)
                for (var x = 0; x < sx + f * cycles; x++)
                    state[z][y][x] = nextState[z][y][x];


                cycleN++;
            }

            var count = state.SelectMany(s => s).SelectMany(s => s).Count(s => s);


            Console.WriteLine(count);
        }

        public void RunPartTwo()
        {
            //var input = ReadTestInput(nameof(Day17));
            var input = ReadInput(nameof(Day17));
            var cycles = 6;
            var state = new List<List<List<List<bool>>>>();
            var nextState = new List<List<List<List<bool>>>>();
            var sx = input[0].Length;
            var sy = input.Length;

            var f = 6;

            for (var w = 0; w < cycles * f + 1; w++)
            {
                state.Add(new List<List<List<bool>>>());
                nextState.Add(new List<List<List<bool>>>());
                for (var z = 0; z < cycles * f + 1; z++)
                {
                    state[w].Add(new List<List<bool>>());
                    nextState[w].Add(new List<List<bool>>());

                    for (var y = 0; y < sy + f * cycles; y++)
                    {
                        state[w][z].Add(new List<bool>());
                        nextState[w][z].Add(new List<bool>());
                        for (var x = 0; x < sx + f * cycles; x++)
                            if (z == cycles*3 && w == cycles*3 && y >= cycles && y < cycles + sy && x >= cycles && x < cycles + sx)
                            {
                                state[w][z][y].Add(input[y - cycles][x - cycles] == '#');
                                nextState[w][z][y].Add(false);
                            }
                            else
                            {
                                state[w][z][y].Add(false);
                                nextState[w][z][y].Add(false);
                            }
                    }
                }
            }

            var cycleN = 0;
            while (cycleN < cycles)
            {
                for (var w = 1; w < cycles * f; w++)
                for (var z = 1; z < cycles * f; z++)
                for (var y = 1; y < sy + f * cycles - 1; y++)
                for (var x = 1; x < sx + f * cycles - 1; x++)
                {
                    var b = state[w][z][y][x];
                    var activeNeigh = 0;
                    for (var dw = -1; dw <= 1; dw++)
                    for (var dz = -1; dz <= 1; dz++)
                    for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        var neigh = state[w + dw][z + dz][y + dy][x + dx];
                        if (neigh && (dw != 0 || dz != 0 || dy != 0 || dx != 0)) activeNeigh++;
                    }

                    if (b)
                    {
                        if (activeNeigh == 2 || activeNeigh == 3)
                            nextState[w][z][y][x] = true;
                        else
                            nextState[w][z][y][x] = false;
                    }
                    else
                    {
                        if (activeNeigh == 3) nextState[w][z][y][x] = true;
                    }
                }

                for (var w = 0; w < cycles * f; w++)
                for (var z = 0; z < cycles * f; z++)
                for (var y = 0; y < sy + f * cycles; y++)
                for (var x = 0; x < sx + f * cycles; x++)
                    state[w][z][y][x] = nextState[w][z][y][x];


                cycleN++;
            }

            var count = state.SelectMany(s => s).SelectMany(s => s).SelectMany(s => s).Count(s => s);


            Console.WriteLine(count);
        }
    }
}
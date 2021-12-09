using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Days.Puzzles
{
    public class Day22 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day22));
            //var input = ReadTestInput(nameof(Day22));

            var player1 = new Queue<int>();
            var player2 = new Queue<int>();

            foreach (var s in input.Skip(1).TakeWhile(s=>!string.IsNullOrEmpty(s)))
            {
                player1.Enqueue(int.Parse(s));
            }

            foreach (var s in input.SkipWhile(s=> s != "Player 2:").Skip(1))
            {
                player2.Enqueue(int.Parse(s));
            }


            while (player1.Count > 0 && player2.Count > 0)
            {
                var cardP1 = player1.Dequeue();
                var cardP2 = player2.Dequeue();

                if (cardP1 > cardP2)
                {
                    player1.Enqueue(cardP1);
                    player1.Enqueue(cardP2);
                }
                if (cardP1 < cardP2)
                {
                    player2.Enqueue(cardP2);
                    player2.Enqueue(cardP1);
                }
            }

            Queue<int> winner = new Queue<int>();

            if (player2.Count > 0)
            {
                winner = player2;
            }

            if (player1.Count > 0)
            {
                winner = player1;
            }


            var score = winner.Count;

            var totalScore = 0L;
            while (score > 0)
            {
                totalScore += winner.Dequeue() * score;
                score--;
            }


            System.Console.WriteLine(totalScore);
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day22));
            //var input = ReadTestInput(nameof(Day22));

            var player1 = new Queue<long>();
            var player2 = new Queue<long>();

            foreach (var s in input.Skip(1).TakeWhile(s => !string.IsNullOrEmpty(s)))
            {
                player1.Enqueue(long.Parse(s));
            }

            foreach (var s in input.SkipWhile(s => s != "Player 2:").Skip(1))
            {
                player2.Enqueue(long.Parse(s));
            }

            var isPlayer1Winner = PlayGame(player1, player2);

            Queue<long> win = new Queue<long>();

            if (isPlayer1Winner)
            {
                win = player1;
            }
            else
            {
                win = player2;
            }

            var score = win.Count;

            var totalScore = 0L;
            while (score > 0)
            {
                totalScore += win.Dequeue() * score;
                score--;
            } 

            System.Console.WriteLine(totalScore);
        }


        private bool PlayGame(Queue<long> player1, Queue<long> player2)
        {
            var prevState = new HashSet<(long, long)>();
            
            while (player1.Count > 0 && player2.Count > 0)
            {
                var state = (SaveState(new Queue<long>(player1)), SaveState(new Queue<long>(player2)));
                if (!prevState.Add(state))
                {
                    return true;
                }

                var cardP1 = player1.Dequeue();
                var cardP2 = player2.Dequeue();

                if (cardP1 <= player1.Count && cardP2 <= player2.Count)
                {
                    var didPlayerWin = PlayGame(new Queue<long>(player1.Take((int)cardP1)), new Queue<long>(player2.Take((int)cardP2)));
                    if (didPlayerWin)
                    {
                        player1.Enqueue(cardP1);
                        player1.Enqueue(cardP2);
                    }
                    else
                    {
                        player2.Enqueue(cardP2);
                        player2.Enqueue(cardP1);
                    }
                    continue;
                }

                if (cardP1 > cardP2)
                {
                    player1.Enqueue(cardP1);
                    player1.Enqueue(cardP2);
                }
                if (cardP1 < cardP2)
                {
                    player2.Enqueue(cardP2);
                    player2.Enqueue(cardP1);
                }

                prevState.Add(state);
            }

            return player1.Count > 0;
        }

        private long SaveState(Queue<long> deck)
        {
            long sum = 0;
            while (deck.Count > 0)
            {
                var tmp = deck.Dequeue();
                sum += tmp * (deck.Count + 1);
            }
            return sum;
        }
    }

    
}
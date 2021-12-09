namespace AoC2020.Days.Puzzles
{
    public class Day25 : Day, IDay
    {
        public void RunPartOne()
        {
            var cardPublicKey = 18356117;
            var doorPublicKey = 5909654;

            // var cardPublicKey = 5764801;
            // var doorPublicKey = 17807724;

            var cardLoopSize = GetLoopSize(cardPublicKey);
            var doorLoopSize = GetLoopSize(doorPublicKey);

            var tDoor = Transform(doorPublicKey, cardLoopSize);
            var tCard = Transform(cardPublicKey, doorLoopSize);

            System.Console.WriteLine(tDoor);
            System.Console.WriteLine(tCard);
        }

        private int GetLoopSize(int publicKey)
        {
            var val = 1;
            var sub = 7;
            var loopSize = 0;
            while (val != publicKey)
            {
                val = val * sub;
                var rem = val % 20201227;
                val = rem;
                loopSize++;
            }

            return loopSize;
        }
        
        private long Transform(int publicKey, int loopSize)
        {
            var val = 1L;
            var sub = publicKey;
            var i = 0;
            while (i < loopSize)
            {
                val = val * sub;
                var rem = val % 20201227;
                val = rem;
                i++;
            }

            return val;
        }

        public void RunPartTwo()
        {
            System.Console.WriteLine();
        }
    }
}
using System;
using System.Numerics;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class CoinFlip
    {
        public Random Random { get; private set; }
        public BigInteger Heads { get; private set; }
        public BigInteger Tails { get; private set; }

        public CoinFlip()
        {
            Random = new Random();
            Heads = 0;
            Tails = 0;
        }

        private CoinFlip(BigInteger heads, BigInteger tails)
        {
            Heads = heads;
            Tails = tails;
        }

        public bool FlippedCoinIsHeads()
        {
            return Random.NextDouble() < 0.5;
        }

        public CoinFlip SimulateCoinFlips(BigInteger timesToFlip)
        {
            for (BigInteger i = 0; i < timesToFlip; i++)
            {
                if (FlippedCoinIsHeads())
                {
                    Heads++;
                }
                else
                {
                    Tails++;
                }
            }

            return new CoinFlip(Heads, Tails);
        }
    }
}

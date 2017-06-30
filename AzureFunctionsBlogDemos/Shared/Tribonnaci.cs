using System.Collections.Generic;
using System.Numerics;
using static AzureFunctionsBlogDemos.Shared.Enums;

namespace AzureFunctionsBlogDemos.Shared
{
    public class Tribonnaci
    {
        public BigInteger[] StartingSet { get; set; }
        public int Iterations { get; set; }

        public BigInteger[] Tribonacci(Tribonnaci input, TribonacciAlgorithms algorithmName)
        {
            switch (algorithmName)
            {
                case TribonacciAlgorithms.linear:
                    return TribonacciLinear(input.StartingSet, input.Iterations);
            }

            return new BigInteger[0];
        }

        public BigInteger[] TribonacciLinear(BigInteger[] signature, int n)
        {
            var list = new List<BigInteger>(signature);

            // hackonacci me
            // if n==0, then return an array of length 1, containing only a 0
            if (n == 0)
            {
                return new BigInteger[1] { 0 };
            }
            else if (n == 1)
            {
                return new BigInteger[1] { signature[0] };
            }
            else if (n == 2)
            {
                return new BigInteger[2] { signature[0], signature[1] };
            }
            else if (n == 3)
            {
                return signature;
            }

            int start = 3;
            BigInteger nMinusOne = list[start - 1];
            BigInteger nMinusTwo = list[start - 2];
            BigInteger nMinusThree = list[start - 3];

            for (int i = 3; i < n; i++)
            {
                list.Add(nMinusOne + nMinusTwo + nMinusThree);
                nMinusOne = list[i];
                nMinusTwo = list[i - 1];
                nMinusThree = list[i - 2];
            }

            return list.ToArray();
        }
    }
}

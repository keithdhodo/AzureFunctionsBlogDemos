using System.Collections.Generic;
using System.Numerics;
using static AzureFunctionsBlogDemos.Shared.Enums;

namespace AzureFunctionsBlogDemos.Shared
{
    public class Tribonnaci
    {
        public BigInteger[] StartingSet { get; set; }
        public int Iterations { get; set; }

        private List<BigInteger> _internalList { get; set; }

        public BigInteger[] Tribonacci(Tribonnaci input, TribonacciAlgorithms algorithmName)
        {
            switch (algorithmName)
            {
                case TribonacciAlgorithms.linear:
                    return TribonacciLinear(input.StartingSet, input.Iterations);
                case TribonacciAlgorithms.quadratic:
                    if (Iterations == 0)
                    {
                        return new BigInteger[1] { 0 };
                    }
                    else if (Iterations == 1)
                    {
                        return new BigInteger[1] { StartingSet[0] };
                    }
                    else if (Iterations == 2)
                    {
                        return new BigInteger[2] { StartingSet[0], StartingSet[1] };
                    }
                    else if (Iterations == 3)
                    {
                        return StartingSet;
                    }
                    else
                    {
                        var _internalList = new List<BigInteger>(StartingSet);

                        for (int i = 3; i < Iterations; i++)
                        {
                            _internalList.Add(0);
                        }
                        _internalList[Iterations - 1] = TribonacciQuadratic(Iterations - 1);
                        return _internalList.ToArray();
                    }
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

        public BigInteger TribonacciQuadratic(int n)
        {
            if (n < 3)
            {
                return StartingSet[n];
            }
            else
            {
                return TribonacciQuadratic(n - 1) + TribonacciQuadratic(n - 2) + TribonacciQuadratic(n - 3);
            }
        }
    }
}

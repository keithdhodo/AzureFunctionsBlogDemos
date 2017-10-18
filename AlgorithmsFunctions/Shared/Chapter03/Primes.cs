using System.Collections.Generic;

// Sieve of Eratosthenes
namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class Primes
    {
        public List<int> FindPrimes(int input)
        {
            var returnList = new List<int>();
            
            if (input > 1)
            {
                bool[] boolArray = new bool[input];

                for (int i = 2; i < input; i++)
                {
                    boolArray[i] = true;
                }

                for (int i = 2; i < input; i++)
                {
                    // easier for readability; granted could just use boolArray[i]
                    if (boolArray[i])
                    {
                        for (int j = i; j * i < input && j * i < int.MaxValue && j * i > 0; j++)
                        {
                            boolArray[i * j] = false;
                        }
                    }
                }

                for (int i = 0; i < input; i++)
                {
                    if (boolArray[i])
                    {
                        returnList.Add(i);
                    }
                }
            }

            return returnList;
        }
    }
}

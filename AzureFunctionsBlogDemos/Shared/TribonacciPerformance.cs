using System.Numerics;

namespace AzureFunctionsBlogDemos.Shared
{
    public class TribonacciPerformance : Performance
    {
        public int Iterations { get; set; }
        public BigInteger Result { get; set; }
    }
}

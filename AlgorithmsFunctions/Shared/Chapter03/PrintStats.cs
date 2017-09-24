using System;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class PrintStats
    {
        private Random random;

        public PrintStats()
        {
            random = new Random();
        }

        public void PrintTheStatistics(double input)
        {
            double mean = 0.0;
            double standardDeviation = 0.0;

            for (int i = 0; i < input; i++)
            {
                var d = random.NextDouble() * 10000;
                mean = d / input;
                standardDeviation += Math.Pow(d, 2) / input;
            }
            standardDeviation = Math.Sqrt(standardDeviation - Math.Pow(mean, 2));
            Console.WriteLine($"\tAvg.: {mean}");
            Console.WriteLine($"Std. dev.: {standardDeviation}");
        }
    }
}

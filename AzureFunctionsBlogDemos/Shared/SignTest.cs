using Microsoft.Azure.WebJobs.Host;
using System;
using System.Numerics;

namespace AzureFunctionsBlogDemos.SignTest
{
    public class SignTest
    {
        public double[] Before { get; set; }
        public double[] After { get; set; }

        public double PValue { get; set; }

        public double ProbabilityOfEffect { get; set; }
        public double ProbabilityOfNoEffect { get; set; }

        public TimeSpan Runtime { get; set; }

        public string AlgorithmName { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public static int[] DoCounts(double[] before, double[] after)
        {
            int[] result = new int[3];  // [0] = fail, [1] = neither, [2] = success
            for (int i = 0; i < before.Length; ++i)
            {
                if (after[i] > before[i])
                    ++result[0];  // fail
                else if (after[i] < before[i])
                    ++result[2];
                else
                    ++result[1];
            }
            return result;
        }

        public static void ShowVector(string pre, double[] v, int dec, string post, TraceWriter log)
        {
            log.Info(pre);
            for (int i = 0; i < v.Length; ++i)
                log.Info(v[i].ToString("F" + dec) + " ");
            log.Info(post);
        }

        public static double BinomProb(int k, int n, double p)
        {
            // probability of getting k "successes" in n trials
            // if p is prob of success on a single trial
            BigInteger c = Choose(n, k);
            double left = Math.Pow(p, k);
            double right = Math.Pow(1.0 - p, n - k);
            return (double)c * left * right;
        }

        public static double BinomRightTail(int k, int n, double p)
        {
            // probability of getting k or more successes in n trials
            double sum = 0.0;
            for (int i = k; i <= n; ++i)
                sum += BinomProb(i, n, p);
            return sum;
        }

        public static BigInteger Choose(int n, int k)
        {
            //if (n < 0 || k < 0)
            //  throw new Exception("Negative argument in Choose");
            //if (n < k) return 0; // special case
            //if (k == 0) return 1; // special case
            if (n == k) return 1; // required special case

            int delta, iMax;

            if (k < n - k) // ex: Choose(100,3)
            {
                delta = n - k;
                iMax = k;
            }
            else           // ex: Choose(100,97)
            {
                delta = k;
                iMax = n - k;
            }

            BigInteger ans = delta + 1;
            for (int i = 2; i <= iMax; ++i)
                ans = (ans * (delta + i)) / i;

            return ans;
        } // Choose
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Chapter03.Tests
{
    [TestClass]
    public class CountToABillionTests
    {
        [TestMethod]
        public void CubeANumberIterativelyTen()
        {
            CubeANumberIteratively(10);
        }

        [TestMethod]
        public void CubeANumberIterativelyHundred()
        {
            CubeANumberIteratively(100);
        }

        [TestMethod]
        public void CubeANumberIterativelyThousand()
        {
            CubeANumberIteratively(1000);
        }

        public TimeSpan CubeANumberIteratively(int nValue)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i, j, k, count = 0;
            for(i = 0; i < nValue; i++)
            {
                for (j = 0; j < nValue; j++)
                {
                    for (k = 0; k < nValue; k++)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}

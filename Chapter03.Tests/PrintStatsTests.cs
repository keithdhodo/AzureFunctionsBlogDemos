using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsFunctions.Shared.Chapter03;

namespace Chapter03.Tests
{
    [TestClass]
    public class PrintStatsTests
    {
        const double ten = 10;

        [TestMethod]
        public void PrintStats_TestPrintTheStatistics10()
        {
            new PrintStats().PrintTheStatistics(Math.Pow(ten, 1));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatistics100()
        {
            new PrintStats().PrintTheStatistics(Math.Pow(ten, 2));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatistics1000()
        {
            new PrintStats().PrintTheStatistics(Math.Pow(ten, 3));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatistics10000()
        {
            new PrintStats().PrintTheStatistics(Math.Pow(ten, 4));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatisticsHudredThousand()
        {
            new PrintStats().PrintTheStatistics(Math.Pow(ten, 5));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatisticsOneMillion()
        {
            new PrintStats().PrintTheStatistics(Math.Pow(ten, 6));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatisticsRandomUpToTenMillion()
        {
            new PrintStats().PrintTheStatistics(new Random().Next(0, (int)Math.Pow(ten, 7)));
        }

        [TestMethod]
        public void PrintStats_TestPrintTheStatisticsRandomUpToOneHundredMillion()
        {
            new PrintStats().PrintTheStatistics(new Random().Next(0, (int)Math.Pow(ten, 8)));
        }
    }
}

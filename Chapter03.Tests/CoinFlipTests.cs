using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsFunctions.Shared.Chapter03;
using System.Numerics;

namespace Chapter03.Tests
{
    [TestClass]
    public class CoinFlipTests
    {
        public CoinFlip CoinFlip { get; private set; }
        public BigInteger Ten { get; private set; }
        public BigInteger NumberOfFlips { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            CoinFlip = new CoinFlip();
            Ten = 10;
        }

        [TestMethod]
        public void CoinFlip_SimulateCoinFlips_Ten()
        {
            NumberOfFlips = BigInteger.Pow(Ten, 1);

            var flips = CoinFlip.SimulateCoinFlips(NumberOfFlips);
            Assert.IsTrue(flips.Heads > 0);
            Assert.IsTrue(flips.Tails > 0);
            Assert.IsTrue(flips.Heads + flips.Tails == NumberOfFlips);
        }

        [TestMethod]
        public void CoinFlip_SimulateCoinFlips_Hundred()
        {
            NumberOfFlips = BigInteger.Pow(Ten, 2);

            var flips = CoinFlip.SimulateCoinFlips(NumberOfFlips);
            Assert.IsTrue(flips.Heads > 0);
            Assert.IsTrue(flips.Tails > 0);
            Assert.IsTrue(flips.Heads + flips.Tails == NumberOfFlips);
        }

        [TestMethod]
        public void CoinFlip_SimulateCoinFlips_Thousand()
        {
            NumberOfFlips = BigInteger.Pow(Ten, 3);

            var flips = CoinFlip.SimulateCoinFlips(NumberOfFlips);
            Assert.IsTrue(flips.Heads > 0);
            Assert.IsTrue(flips.Tails > 0);
            Assert.IsTrue(flips.Heads + flips.Tails == NumberOfFlips);
        }

        [TestMethod]
        public void CoinFlip_SimulateCoinFlips_TenThousand()
        {
            NumberOfFlips = BigInteger.Pow(Ten, 4);

            var flips = CoinFlip.SimulateCoinFlips(NumberOfFlips);
            Assert.IsTrue(flips.Heads > 0);
            Assert.IsTrue(flips.Tails > 0);
            Assert.IsTrue(flips.Heads + flips.Tails == NumberOfFlips);
        }

        [TestMethod]
        public void CoinFlip_SimulateCoinFlips_HundredThousand()
        {
            NumberOfFlips = BigInteger.Pow(Ten, 5);

            var flips = CoinFlip.SimulateCoinFlips(NumberOfFlips);
            Assert.IsTrue(flips.Heads > 0);
            Assert.IsTrue(flips.Tails > 0);
            Assert.IsTrue(flips.Heads + flips.Tails == NumberOfFlips);
        }

        [TestMethod]
        public void CoinFlip_SimulateCoinFlips_Million()
        {
            NumberOfFlips = BigInteger.Pow(Ten, 6);

            var flips = CoinFlip.SimulateCoinFlips(NumberOfFlips);
            Assert.IsTrue(flips.Heads > 0);
            Assert.IsTrue(flips.Tails > 0);
            Assert.IsTrue(flips.Heads + flips.Tails == NumberOfFlips);
        }
    }
}

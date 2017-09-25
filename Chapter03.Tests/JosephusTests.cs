using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsFunctions.Shared.Chapter03;
using System;

namespace Chapter03.Tests
{
    [TestClass]
    public class JosephusTests
    {

        [TestMethod]
        public void Josephus_TestNineParticipantsAndEliminateByFive()
        {
            var result = new Josephus(numberOfParticipants: 9, orderToRemoveParticipants: 5).ExecuteJosephusSimulation();
            Assert.AreEqual(8, result.Item);
        }

        [TestMethod]
        public void Josephus_TestOneThousandParticipantsAndEliminateByTwo()
        {
            var result = new Josephus(numberOfParticipants: (int)Math.Pow(10, 3), orderToRemoveParticipants: 2).ExecuteJosephusSimulation();
            Assert.AreEqual(999, result.Item);
        }

        [TestMethod]
        public void Josephus_TestTenThousandParticipantsAndEliminateByThree()
        {
            var result = new Josephus(numberOfParticipants: (int)Math.Pow(10, 4), orderToRemoveParticipants: 3).ExecuteJosephusSimulation();
            Assert.AreEqual(9999, result.Item);
        }

        [TestMethod]
        public void Josephus_TestOneHundredThousandParticipantsAndEliminateByFive()
        {
            var result = new Josephus(numberOfParticipants: (int)Math.Pow(10, 5), orderToRemoveParticipants: 5).ExecuteJosephusSimulation();
            Assert.AreEqual(99999, result.Item);
        }

        [TestMethod]
        public void Josephus_TestOneMillionParticipantsAndEliminateByTen()
        {
            var result = new Josephus(numberOfParticipants: (int)Math.Pow(10, 6), orderToRemoveParticipants: 10).ExecuteJosephusSimulation();
            Assert.AreEqual(999999, result.Item);
        }

        [TestMethod]
        public void Josephus_TestOneMillionParticipantsAndEliminateByFifty()
        {
            var result = new Josephus(numberOfParticipants: (int)Math.Pow(10, 6), orderToRemoveParticipants: 50).ExecuteJosephusSimulation();
            Assert.AreEqual(999999, result.Item);
        }
    }
}

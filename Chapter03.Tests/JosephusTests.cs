using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsFunctions.Shared.Chapter03;

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
    }
}

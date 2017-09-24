using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsFunctions.Shared.Chapter03;

namespace Chapter03.Tests
{
    [TestClass]
    public class LogBaseTwoTests
    {

        [TestMethod]
        public void LogBaseTwo_TenThousand()
        {
            var logClass = new LogBaseTwo();
            Assert.AreEqual(14, logClass.Log(10000));
        }

        [TestMethod]
        public void LogBaseTwo_OneHundredThousand()
        {
            var logClass = new LogBaseTwo();
            Assert.AreEqual(17, logClass.Log(100000));
        }

        [TestMethod]
        public void LogBaseTwo_OneMillion()
        {
            var logClass = new LogBaseTwo();
            Assert.AreEqual(20, logClass.Log(1000000));
        }
    }
}

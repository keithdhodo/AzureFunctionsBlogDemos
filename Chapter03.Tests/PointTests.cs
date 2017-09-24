using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03.Tests
{
    [TestClass]
    public class PointTests
    {
        Point zeroZero;
        Point tenTen;

        [TestInitialize]
        public void Initialize()
        {
            zeroZero = new Point(0, 0);
            tenTen = new Point(10, 10);
        }

        [TestMethod]
        public void Point_TestDefaultConstructor()
        {
            var defaultConstructor = new Point();
            Assert.IsTrue(defaultConstructor.X > 0);
            Assert.IsTrue(defaultConstructor.Y > 0);
        }

        [TestMethod]
        public void Point_TestOverloadedConstructor_InputZero()
        {
            Assert.IsTrue(zeroZero.X == 0);
            Assert.IsTrue(zeroZero.Y == 0);
        }

        [TestMethod]
        public void Point_TestOverloadedConstructor_LargeNegative()
        {
            var largeNegative = new Point(double.MinValue, double.MinValue);
            Assert.IsTrue(largeNegative.X == double.MinValue);
            Assert.IsTrue(largeNegative.Y == double.MinValue);
        }

        [TestMethod]
        public void Point_TestOverloadedConstructor_LargePositive()
        {
            var largePositive = new Point(double.MaxValue, double.MaxValue);
            Assert.IsTrue(largePositive.X == double.MaxValue);
            Assert.IsTrue(largePositive.Y == double.MaxValue);
        }

        [TestMethod]
        public void Point_TestRadius_InputTen()
        {
            Assert.AreEqual(14.142135623730951, tenTen.Radius());
        }

        [TestMethod]
        public void Point_TestTheta_InputTen()
        {
            Assert.AreEqual(0.78539816339744828, tenTen.Theta());
        }

        [TestMethod]
        public void Point_TestDistance_InputZeroTen()
        {
            Assert.AreEqual(14.142135623730951, tenTen.Distance(zeroZero));
        }

        [TestMethod]
        public void Point_TestToString_InputZeroTen()
        {
            var tenTenToString = tenTen.ToString();
            Assert.AreEqual("(10, 10)", tenTen.ToString());
        }
    }
}

using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chapter03.Tests
{
    [TestClass]
    public class ClosePointsTests
    {
        public ClosePoints ClosePoints { get; private set; }

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void ClosePoints_PointFurtherThanDistanceReturnsZero()
        {
            ClosePoints = new ClosePoints(2);
            ClosePoints.Points[0] = new Point() { X = 0, Y = 0 };
            ClosePoints.Points[1] = new Point() { X = 0, Y = 10 };

            var count = ClosePoints.FindPointsWithinDistance(5);
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void ClosePoints_PointWithinDistanceReturnsOne()
        {
            ClosePoints = new ClosePoints(2);
            ClosePoints.Points[0] = new Point() { X = 0, Y = 0 };
            ClosePoints.Points[1] = new Point() { X = 0, Y = 4 };

            var count = ClosePoints.FindPointsWithinDistance(5);
            Assert.AreEqual(1, count);
        }
    }
}

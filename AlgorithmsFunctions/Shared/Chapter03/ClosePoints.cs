using System;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class ClosePoints
    {
        public Random Random { get; private set; }
        public Point[] Points { get; private set; }
        public int NumberOfPoints { get; private set; }

        private ClosePoints()
        {

        }

        public ClosePoints(int numberOfPoints) : this()
        {
            Random = new Random();
            NumberOfPoints = numberOfPoints;
            Points = new Point[NumberOfPoints];
        }

        public int FindPointsWithinDistance(int distance)
        {
            var pointsWithinDistance = 0;

            for (int i = 0; i < Points.Length; i++)
            {
                for (int j = i + 1; j < Points.Length; j++)
                {
                    if (Points[i].Distance(Points[j]) < distance)
                    {
                        pointsWithinDistance++;
                    }
                }
            }

            return pointsWithinDistance;
        }

        public void GenerateRandomPoints()
        {
            for (int i = 0; i < NumberOfPoints; i++)
            {
                Points[i] = new Point()
                {
                    X = Random.Next(-NumberOfPoints, NumberOfPoints),
                    Y = Random.Next(-NumberOfPoints, NumberOfPoints)
                };
            }

            return;
        }
    }
}

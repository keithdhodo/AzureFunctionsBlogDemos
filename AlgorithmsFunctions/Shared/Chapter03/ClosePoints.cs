using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class ClosePoints
    {
        public Random Random { get; private set; }
        public Point[] Points { get; private set; }

        public ClosePoints()
        {
            Random = new Random();
        }

        public int FindPointsWithinDistance(int distance)
        {
            var numberOfPoints = distance * 4;

            Points = new Point[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                Points[i] = new Point()
                {
                    X = Random.Next(-numberOfPoints, numberOfPoints),
                    Y = Random.Next(-numberOfPoints, numberOfPoints)
                };
            }

            // calculate the number of points within distance
            var count = 0;

            for (int i = 0; i < numberOfPoints; i++)
            {
                for (int j = i+1; j < numberOfPoints; j++)
                {
                    if (Points[i].Distance(Points[j]) < distance)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}

using System;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        private Random random;  

        public Point()
        {
            random = new Random();
            X = random.NextDouble();
            Y = random.NextDouble();
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Radius()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        public double Theta()
        {
            return Math.Atan2(Y, X);
        }

        public double Distance(Point point)
        {
            double dX = X - point.X;
            double dY = Y - point.Y;
            return Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}

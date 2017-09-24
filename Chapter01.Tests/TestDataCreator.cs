using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmsFunctions.Tests
{
    [TestClass]
    public class TestDataCreator
    {
        public void CreateIntegers(int input)
        {
            Random r = new Random();

            for (int i = 0; i < input; i++)
            {
                int number = r.Next(0, input * 2);
                Console.WriteLine(number);
            }
        }

        [Ignore]
        [TestMethod]
        public void CreateIntegers()
        {
            CreateIntegers(1000000);
        }
    }
}

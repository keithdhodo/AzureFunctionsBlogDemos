using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmsFunctions.Tests
{
    [TestClass]
    public class TestDataCreator
    {
        [TestMethod]
        public void CreateIntegers()
        {
            CreateIntegersCommaSeparated(50);
            CreateIntegersNewline(1000000);
        }

        private void CreateIntegersCommaSeparated(int input)
        {
            Random r = new Random();

            for (int i = 0; i < input; i++)
            {
                int number = r.Next(50, input * 4);
                
                if (i < input - 1)
                    Console.Write(number + ", ");
                else
                { 
                    Console.Write(number);
                    Console.WriteLine();
                }
            }
        }

        private void CreateIntegersNewline(int input)
        {
            Random r = new Random();

            for (int i = 0; i < input; i++)
            {
                int number = r.Next(0, input * 2);
                Console.WriteLine(number);
            }
        }
    }
}
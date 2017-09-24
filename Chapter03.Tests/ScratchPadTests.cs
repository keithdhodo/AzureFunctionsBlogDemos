using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Chapter03.Tests
{
    [TestClass]
    public class ScratchPadTests
    {
        [TestMethod]
        public void Chapter03_ScratchPad_TestArrays()
        {
            var array = new int[99];

            Console.WriteLine("Array values for first loop: ");

            for (int i = 0; i < 99; i++)
            {
                array[i] = 98 - i;
                Console.Write($"array[{i}]: {array[i]}, ");
            }

            Console.WriteLine("\n\nArray values for second loop: ");

            for (int i = 0; i < 99; i++)
            {
                array[i] = array[array[i]];
                Console.Write($"array[{i}]: {array[i]}, ");
            }
        }
    }
}

using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Chapter03.Tests
{
    [TestClass]
    public class PrimesTests
    {
        public Primes Primes { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            Primes = new Primes();
        }

        [TestMethod]
        public void Primes_TestPrimesLessThanFive()
        {
            var expectedList = new List<int>() { 2, 3 };
            CollectionAssert.AreEqual(expectedList, Primes.FindPrimes(5));
        }

        [TestMethod]
        public void Primes_TestPrimesLessThanOneHundred()
        {
            var expectedList = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            CollectionAssert.AreEqual(expectedList, Primes.FindPrimes((int)Math.Pow(10, 2)));
        }

        [TestMethod]
        public void Primes_TestPrimesLessThanTenThousand()
        {
            string path = Path.Combine("../../../", @"TestData/Data/PrimesLowerThanTenThousand.csv");
            var expectedList = new List<int>(CommaSeparatedValueParser.CsvToListOfIntegers(path));

            CollectionAssert.AreEqual(expectedList, Primes.FindPrimes((int)Math.Pow(10, 4)));
        }

        [TestMethod]
        public void Primes_TestPrimesLessThanOneMillion()
        {
            string path = Path.Combine("../../../", @"TestData/Data/PrimesLowerThanOneMillion.csv");
            var expectedList = new List<int>(CommaSeparatedValueParser.CsvToListOfIntegers(path));

            CollectionAssert.AreEqual(expectedList, Primes.FindPrimes((int)Math.Pow(10, 6)));
        }

        [TestMethod]
        public void Primes_TestPrimesLessThanTenMillion()
        {
            string path = Path.Combine("../../../", @"TestData/Data/PrimesLowerThanTenMillion.csv");
            var expectedList = new List<int>(CommaSeparatedValueParser.CsvToListOfIntegers(path));

            CollectionAssert.AreEqual(expectedList, Primes.FindPrimes((int)Math.Pow(10, 7)));
        }

        //[TestMethod]
        //public void Primes_TestPrimesLessThanIntMax()
        //{
        //    var primeArray = Primes.FindPrimes((int)Math.Pow(10, 7)).ToArray();

        //    for (int i = 0; i < primeArray.Length; i++)
        //    {
        //        Console.Write(primeArray[i] + ", ");
        //    }

        //    string path = Path.Combine("../../../", @"TestData/Data/PrimesLowerThanOneMillion.csv");
        //    var expectedList = new List<int>(CommaSeparatedValueParser.CsvToListOfIntegers(path));

        //    CollectionAssert.AreEqual(expectedList, Primes.FindPrimes((int)Math.Pow(10, 6)));
        //}
    }
}
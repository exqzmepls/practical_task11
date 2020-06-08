using System;
using System.IO;
using practical_task11;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIntInput()
        {
            Console.SetIn(new StreamReader("intInput.txt"));
            double result = 2;

            double input = Program.IntInput(lBound: 0, uBound: 100, info: "some info");

            Assert.AreEqual(result, input);
        }

        [TestMethod]
        public void TestCheckMatrixInputFalse1()
        {
            bool result = Program.CheckMatrixInput(new string[] { "1 0 1 0", "0 0 1 0", "10 10", "a b c d" }, out bool[,] matr);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestCheckMatrixInputFalse2()
        {
            bool result = Program.CheckMatrixInput(new string[] { "1 0 1 0", "0 0 1 0", "1 1 1 0", "a b c d" }, out bool[,] matr);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestCheckMatrixInputFalse3()
        {
            bool result = Program.CheckMatrixInput(new string[] { "1 0 1 0", "0 0 1 0", "1 1 1 0", "0 3 0 0" }, out bool[,] matr);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestCheckMatrixInputFalse4()
        {
            bool result = Program.CheckMatrixInput(new string[] { "1 0 1 0", "0 0 1 0", "1 1 1 0", "0 -7 0 0" }, out bool[,] matr);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestCheckMatrixInputTrue()
        {
            bool result = Program.CheckMatrixInput(new string[] { "1 0 1 0", "0 0 1 0", "1 1 1 0", "0 0 0 0"}, out bool[,] matr);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestIsCipherFalse()
        {
            bool result = Program.CheckMatrixInput(new string[] { "1 1 1 1 1 0", "1 1 0 1 1 1", "0 1 1 1 1 0", "1 1 0 1 0 1", "0 1 0 1 0 1",  "1 0 1 1 1 1"}, out bool[,] matr);
            result = result && Program.IsCipher(matr);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestIsCipherTrue()
        {
            int SIZE = 10;
            Console.SetIn(new StreamReader("cipher.txt"));
            string[] input = new string[SIZE];
            for (int i = 0; i < SIZE; i++) input[i] = Console.ReadLine();
            bool result = Program.CheckMatrixInput(input, out bool[,] matr);
            result = result && Program.IsCipher(matr);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestCoding()
        {
            int SIZE = 10;
            string text = "В одном из домов, населения которого стало бы на уездный город, лежал в постели, Илья Ильич Обломов.";
            string code = "Вите яезо лддкнино,ыо тйм о риоИзг ольрдооягод с тИальмичло, во ,л еб жОнбалалыос  внем лаопенво у.с";
            Console.SetIn(new StreamReader("cipher.txt"));
            string[] input = new string[SIZE];
            for (int i = 0; i < SIZE; i++) input[i] = Console.ReadLine();
            bool result = Program.CheckMatrixInput(input, out bool[,] matr);
            result = result && string.Join("", Program.Coding(matr, text)) == code;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestDecoding()
        {
            int SIZE = 10;
            string decode = "В одном из домов, населения которого стало бы на уездный город, лежал в постели, Илья Ильич Обломов.";
            string text = "Вите яезо лддкнино,ыо тйм о риоИзг ольрдооягод с тИальмичло, во ,л еб жОнбалалыос  внем лаопенво у.с";
            Console.SetIn(new StreamReader("cipher.txt"));
            string[] input = new string[SIZE];
            for (int i = 0; i < SIZE; i++) input[i] = Console.ReadLine();
            bool result = Program.CheckMatrixInput(input, out bool[,] matr);
            result = result && string.Join("", Program.Decoding(matr, text)) == decode;
            Assert.AreEqual(true, result);
        }
    }
}

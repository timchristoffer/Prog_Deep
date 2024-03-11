using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NUnit.Framework;


namespace StringCalculatorKata.Test
{
    public class StringCalculatorTest
    {
        [Test]
        public void Add_EmptyStringAsParam_ReturnsZero()
        {
            Assert.AreEqual(0, StringCalculator.Add(""));
        }

        [Test]
        public void Add_SingleNumbers_ReturnOne()
        {
            Assert.AreEqual(1, StringCalculator.Add("1"));
        }

        [Test]
        public void Add_TwoNumbers_ReturnTwo()
        {
            Assert.AreEqual(3, StringCalculator.Add("1, 2"));
        }

        [Test]
        public void Add_UnknownNumbers_ReturnSum()
        {
            Assert.AreEqual(150, StringCalculator.Add("100, 50"));
        }

        [Test]
        public void Add_MultipleNumbers_ReturnSum()
        {
            Assert.AreEqual(6, StringCalculator.Add("1\n2,3"));
        }

    }
}
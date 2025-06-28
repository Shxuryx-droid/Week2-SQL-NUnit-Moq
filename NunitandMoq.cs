using NUnit.Framework;
using CalculatorLib;

namespace CalculatorLibTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
        }

        [TearDown]
        public void TearDown()
        {
            _calculator = null;
        }

        [Test]
        [TestCase(2, 3, 5)]
        [TestCase(0, 0, 0)]
        [TestCase(-1, 1, 0)]
        public void Add_ShouldReturnExpectedResult(int a, int b, int expected)
        {
            var result = _calculator.Add(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Ignore("Not implemented yet")]
        public void Subtract_TestIgnored()
        {
            Assert.Pass();
        }
    }
}

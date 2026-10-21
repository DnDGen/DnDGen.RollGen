using DnDGen.RollGen.Expressions;
using Moq;
using NUnit.Framework;

namespace DnDGen.RollGen.Tests.Unit.Expressions
{
    [TestFixture]
    public class ExpressionEvaluatorNumericProxyTests
    {
        private ExpressionEvaluator proxy;
        private Mock<ExpressionEvaluator> mockInnerEvaluator;

        [SetUp]
        public void Setup()
        {
            mockInnerEvaluator = new Mock<ExpressionEvaluator>();
            proxy = new ExpressionEvaluatorNumericProxy(mockInnerEvaluator.Object);
        }

        [TestCase("0", 0)]
        [TestCase("5", 5)]
        [TestCase("-8", -8)]
        [TestCase("+5", 5)]
        [TestCase("007", 7)]
        [TestCase("0.25", 0.25)]
        [TestCase("-0.25", -0.25)]
        [TestCase(" 9266 ", 9266)]
        [TestCase("9266.90210", 9266.9021)]
        [TestCase("10000000000", 10000000000)]
        [TestCase(".5", 0.5)]
        [TestCase("-.5", -0.5)]
        [TestCase("+.5", 0.5)]
        [TestCase("5.", 5)]
        [TestCase("1e5", 100000)]
        [TestCase("NaN", double.NaN)]
        [TestCase("Infinity", double.PositiveInfinity)]
        [TestCase("-Infinity", double.NegativeInfinity)]
        public void Evaluate_ReturnsNumber_WhenExpressionIsNumber(string expression, double expected)
        {
            var result = proxy.Evaluate<double>(expression);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("0")]
        [TestCase("5")]
        [TestCase("-8")]
        [TestCase("+5")]
        [TestCase("007")]
        [TestCase("0.25")]
        [TestCase("-0.25")]
        [TestCase(" 9266 ")]
        [TestCase("9266.90210")]
        [TestCase("10000000000")]
        [TestCase(".5")]
        [TestCase("-.5")]
        [TestCase("+.5")]
        [TestCase("5.")]
        [TestCase("1e5")]
        [TestCase("NaN")]
        [TestCase("Infinity")]
        [TestCase("-Infinity")]
        public void Evaluate_DoesNotCallInnerEvaluator_WhenExpressionIsNumber(string expression)
        {
            proxy.Evaluate<double>(expression);
            mockInnerEvaluator.VerifyNoOtherCalls();
        }

        [Test]
        public void Evaluate_ConvertsNumberToRequestedType()
        {
            var result = proxy.Evaluate<int>("902.1");
            Assert.That(result, Is.EqualTo(902));
        }

        [TestCase("0", false)]
        [TestCase("5", true)]
        [TestCase("-8", true)]
        public void Evaluate_ReturnsBoolean_WhenExpressionIsNumber(string expression, bool expected)
        {
            var result = proxy.Evaluate<bool>(expression);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [SetCulture("de-DE")]
        public void Evaluate_ReturnsNumber_RegardlessOfCurrentCulture()
        {
            var result = proxy.Evaluate<double>("0.25");
            Assert.That(result, Is.EqualTo(0.25));
        }

        [TestCase("1+2")]
        [TestCase("min(1, 2)")]
        [TestCase("- 5")]
        [TestCase("(5)")]
        [TestCase("1,000")]
        [TestCase("14,10")]
        [TestCase("")]
        public void Evaluate_ReturnsInnerResult_WhenExpressionIsNotNumber(string expression)
        {
            mockInnerEvaluator.Setup(e => e.Evaluate<double>(expression)).Returns(9266);

            var result = proxy.Evaluate<double>(expression);
            Assert.That(result, Is.EqualTo(9266));
        }

        [TestCase("0")]
        [TestCase("5")]
        [TestCase("-8")]
        [TestCase("+5")]
        [TestCase("007")]
        [TestCase("0.25")]
        [TestCase("-0.25")]
        [TestCase(" 9266 ")]
        [TestCase("9266.90210")]
        [TestCase("10000000000")]
        [TestCase(".5")]
        [TestCase("-.5")]
        [TestCase("+.5")]
        [TestCase("5.")]
        [TestCase("1e5")]
        [TestCase("NaN")]
        [TestCase("Infinity")]
        [TestCase("-Infinity")]
        public void IsValid_ReturnsTrue_WhenExpressionIsNumber(string expression)
        {
            var result = proxy.IsValid(expression);
            Assert.That(result, Is.True);
        }

        [TestCase("0")]
        [TestCase("5")]
        [TestCase("-8")]
        [TestCase("+5")]
        [TestCase("007")]
        [TestCase("0.25")]
        [TestCase("-0.25")]
        [TestCase(" 9266 ")]
        [TestCase("9266.90210")]
        [TestCase("10000000000")]
        [TestCase(".5")]
        [TestCase("-.5")]
        [TestCase("+.5")]
        [TestCase("5.")]
        [TestCase("1e5")]
        [TestCase("NaN")]
        [TestCase("Infinity")]
        [TestCase("-Infinity")]
        public void IsValid_DoesNotCallInnerEvaluator_WhenExpressionIsNumber(string expression)
        {
            proxy.IsValid(expression);
            mockInnerEvaluator.VerifyNoOtherCalls();
        }

        [TestCase("1+2", true)]
        [TestCase("1+2", false)]
        [TestCase("min(1, 2)", true)]
        [TestCase("min(1, 2)", false)]
        [TestCase("- 5", true)]
        [TestCase("- 5", false)]
        [TestCase("(5)", true)]
        [TestCase("(5)", false)]
        [TestCase("1,000", true)]
        [TestCase("1,000", false)]
        [TestCase("14,10", true)]
        [TestCase("14,10", false)]
        [TestCase("", true)]
        [TestCase("", false)]
        public void IsValid_ReturnsInnerResult_WhenExpressionIsNotNumber(string expression, bool innerResult)
        {
            mockInnerEvaluator.Setup(e => e.IsValid(expression)).Returns(innerResult);

            var result = proxy.IsValid(expression);
            Assert.That(result, Is.EqualTo(innerResult));
        }
    }
}

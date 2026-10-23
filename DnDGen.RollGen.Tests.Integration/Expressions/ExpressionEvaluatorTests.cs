using DnDGen.RollGen.Expressions;
using DnDGen.RollGen.Tests.Integration.TestData;
using NUnit.Framework;

namespace DnDGen.RollGen.Tests.Integration
{
    [TestFixture]
    public class ExpressionEvaluatorTests : IntegrationTests
    {
        private ExpressionEvaluator expressionEvaluator;

        [SetUp]
        public void Setup()
        {
            expressionEvaluator = GetNewInstanceOf<ExpressionEvaluator>();
        }

        [TestCaseSource(typeof(EvaluatorTestData), nameof(EvaluatorTestData.ValuesTestCases))]
        public void Evaluate_ReturnsValue(string expression, double expected)
        {
            var actual = expressionEvaluator.Evaluate<double>(expression);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(EvaluatorTestData), nameof(EvaluatorTestData.BooleansTestCases))]
        public void Evaluate_ReturnsBooleanValue(string expression, bool expected)
        {
            var actual = expressionEvaluator.Evaluate<bool>(expression);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(typeof(EvaluatorTestData), nameof(EvaluatorTestData.ValuesTestCases))]
        public void IsValid_ReturnsTrue_ForValueExpression(string expression, double value)
        {
            var actual = expressionEvaluator.IsValid(expression);
            Assert.That(actual, Is.True);
        }

        [TestCaseSource(typeof(EvaluatorTestData), nameof(EvaluatorTestData.BooleansTestCases))]
        public void IsValid_ReturnsTrue_ForBooleanExpression(string expression, bool value)
        {
            var actual = expressionEvaluator.IsValid(expression);
            Assert.That(actual, Is.True);
        }

        [TestCase("- 5", true)]
        [TestCase("1,000", false)]
        [TestCase("NaN", true)]
        [TestCase("Infinity", true)]
        [TestCase("(9266, 90210, 42, 600, 1337, 1336, 96, 783, 8245, 922, 2022, 227)", false, Ignore = "Albatross interprets this as an array")]
        [TestCase("9266, 90210, 42, 600, 1337, 1336, 96, 783, 8245, 922, 2022, 227", false)]
        [TestCase("coalesce(null, null, 9266, 90210, 42, 600, 1337, 1336, 96, 783, 8245, 922, 2022, 227)", true,
            Ignore = "Coalesce function seems to be no longer supported by Albatross.Expression as of version 5.X")]
        [TestCase("bad(9266, 90210, 42, 600, 1337, 1336, 96, 783, 8245, 922, 2022, 227)", false)]
        [TestCase("9266 =< 90210", false)]
        [TestCase("9266 =< 9266", false)]
        [TestCase("9266 =< 42", false)]
        [TestCase("9266 => 90210", false)]
        [TestCase("9266 => 9266", false)]
        [TestCase("9266 => 42", false)]
        [TestCase("9266 >< 90210", false)]
        [TestCase("9266 >< 9266", false)]
        [TestCase("9266 >< 42", false)]
        [TestCase("9266 == 90210", false)]
        [TestCase("9266 == 9266", false)]
        [TestCase("9266 == 42", false)]
        [TestCase("1d6", false)]
        [TestCase("1d6+3", false)]
        [TestCase("1 d 6", false)]
        [TestCase("xdy", false)]
        [TestCase("invalid", false)]
        [TestCase("this is not a roll", false)]
        public void IsValid_ReturnsValidity(string expression, bool expected)
        {
            var actual = expressionEvaluator.IsValid(expression);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}

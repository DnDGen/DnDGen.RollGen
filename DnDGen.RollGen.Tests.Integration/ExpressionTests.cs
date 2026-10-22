using DnDGen.RollGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.RollGen.Tests.Integration
{
    [TestFixture]
    public class ExpressionTests : IntegrationTests
    {
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.NumbersTestCases))]
        public void IsValid_ReturnsTrue_ForNumber(string expression, double value)
        {
            var valid = dice.Roll(expression).IsValid();
            Assert.That(valid, Is.True);
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.NumbersTestCases))]
        public void AsSum_ReturnsNumber(string expression, double value)
        {
            var sum = dice.Roll(expression).AsSum<double>();
            Assert.That(sum, Is.EqualTo(value));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.NumbersTestCases))]
        public void AsPotentialMinimum_ReturnsNumber(string expression, double value)
        {
            var minimum = dice.Roll(expression).AsPotentialMinimum<double>();
            Assert.That(minimum, Is.EqualTo(value));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.NumbersTestCases))]
        public void AsPotentialMaximum_ReturnsNumber(string expression, double value)
        {
            var maximum = dice.Roll(expression).AsPotentialMaximum<double>();
            Assert.That(maximum, Is.EqualTo(value));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.NumbersTestCases))]
        public void AsPotentialAverage_ReturnsNumber(string expression, double value)
        {
            var average = dice.Roll(expression).AsPotentialAverage();
            Assert.That(average, Is.EqualTo(value));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.NumbersTestCases))]
        public void AsIndividualRolls_ReturnsOnlyNumber(string expression, double value)
        {
            var rolls = dice.Roll(expression).AsIndividualRolls<double>();
            Assert.That(rolls, Is.EqualTo(new[] { value }));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ExpressionsTestCases))]
        public void IsValid_ReturnsTrue_ForExpression(string expression, double minimum, double maximum, int minimumRolls, int maximumRolls)
        {
            var valid = dice.Roll(expression).IsValid();
            Assert.That(valid, Is.True);
        }

        [Repeat(100)]
        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ExpressionsTestCases))]
        public void AsSum_ReturnsValueWithinPotentialRange(string expression, double minimum, double maximum, int minimumRolls, int maximumRolls)
        {
            var sum = dice.Roll(expression).AsSum<double>();
            Assert.That(sum, Is.InRange(minimum, maximum));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ExpressionsTestCases))]
        public void AsPotentialMinimum_ReturnsMinimum(string expression, double minimum, double maximum, int minimumRolls, int maximumRolls)
        {
            var potentialMinimum = dice.Roll(expression).AsPotentialMinimum<double>();
            Assert.That(potentialMinimum, Is.EqualTo(minimum));
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ExpressionsTestCases))]
        public void AsPotentialMaximum_ReturnsMaximum(string expression, double minimum, double maximum, int minimumRolls, int maximumRolls)
        {
            var potentialMaximum = dice.Roll(expression).AsPotentialMaximum<double>();
            Assert.That(potentialMaximum, Is.EqualTo(maximum));
        }

        //HACK: We are ignoring the average, since chained rolls can produce decimal rolls during evaluation (#109)

        [Repeat(100)]
        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ExpressionsTestCases))]
        public void AsIndividualRolls_ReturnsExpectedCount(string expression, double minimum, double maximum, int minimumRolls, int maximumRolls)
        {
            var rolls = dice.Roll(expression).AsIndividualRolls<double>();
            Assert.That(rolls.Count(), Is.InRange(minimumRolls, maximumRolls));
        }

        [Repeat(100)]
        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ExpressionsTestCases))]
        public void AsIndividualRolls_SumWithinPotentialRange(string expression, double minimum, double maximum, int minimumRolls, int maximumRolls)
        {
            var rolls = dice.Roll(expression).AsIndividualRolls<double>();
            Assert.That(rolls.Sum(), Is.InRange(minimum, maximum));
        }
    }
}

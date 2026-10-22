using DnDGen.RollGen.Tests.Integration.TestData;
using NUnit.Framework;
using System;
using System.Linq;

namespace DnDGen.RollGen.Tests.Integration.Stress
{
    [TestFixture]
    public class ExpressionTests : StressTests
    {
        private Dice dice;
        private Random random;
        private (string Expression, double Value)[] numbers;
        private (string Expression, double Minimum, double Maximum, int MinimumRolls, int MaximumRolls)[] expressions;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
            random = GetNewInstanceOf<Random>();
            numbers = [.. ExpressionTestData.Numbers];
            expressions = [.. ExpressionTestData.Expressions];
        }

        //INFO: RollGen converts decimal results to int with Convert, which rounds half to even
        private static int ToInt(double value) => Convert.ToInt32(value);

        [Test]
        public void StressNumber()
        {
            stressor.Stress(AssertNumber);
        }

        private void AssertNumber()
        {
            var (expression, value) = numbers[random.Next(numbers.Length)];
            var roll = dice.Roll(expression);

            Assert.That(roll.IsValid(), Is.True, roll.CurrentRollExpression);
            Assert.That(roll.AsSum(), Is.EqualTo(ToInt(value)), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMinimum(), Is.EqualTo(ToInt(value)), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMaximum(), Is.EqualTo(ToInt(value)), roll.CurrentRollExpression);
            AssertTrueOrFalse(roll);
            Assert.That(roll.AsIndividualRolls(), Is.EqualTo([ToInt(value)]), roll.CurrentRollExpression);
        }

        [Test]
        public void StressNumberAsDouble()
        {
            stressor.Stress(AssertNumberAsDouble);
        }

        private void AssertNumberAsDouble()
        {
            var (expression, value) = numbers[random.Next(numbers.Length)];
            var roll = dice.Roll(expression);

            Assert.That(roll.IsValid(), Is.True, roll.CurrentRollExpression);
            Assert.That(roll.AsSum<double>(), Is.EqualTo(value), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMinimum<double>(), Is.EqualTo(value), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMaximum<double>(), Is.EqualTo(value), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialAverage(), Is.EqualTo(value), roll.CurrentRollExpression);
            AssertTrueOrFalse(roll);
            Assert.That(roll.AsIndividualRolls<double>(), Is.EqualTo([value]), roll.CurrentRollExpression);
        }

        [Test]
        public void StressExpression()
        {
            stressor.Stress(AssertExpression);
        }

        private void AssertExpression()
        {
            var (expression, minimum, maximum, minimumRolls, maximumRolls) = expressions[random.Next(expressions.Length)];
            var roll = dice.Roll(expression);

            Assert.That(roll.IsValid(), Is.True, roll.CurrentRollExpression);
            Assert.That(roll.AsSum(), Is.InRange(ToInt(minimum), ToInt(maximum)), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMinimum(), Is.EqualTo(ToInt(minimum)), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMaximum(), Is.EqualTo(ToInt(maximum)), roll.CurrentRollExpression);
            AssertTrueOrFalse(roll);

            var rolls = roll.AsIndividualRolls();
            Assert.That(rolls.Count(), Is.InRange(minimumRolls, maximumRolls), roll.CurrentRollExpression);
            Assert.That(rolls.Sum(), Is.InRange(ToInt(minimum), ToInt(maximum)), roll.CurrentRollExpression);
        }

        [Test]
        public void StressExpressionAsDouble()
        {
            stressor.Stress(AssertExpressionAsDouble);
        }

        private void AssertExpressionAsDouble()
        {
            var (expression, minimum, maximum, minimumRolls, maximumRolls) = expressions[random.Next(expressions.Length)];
            var roll = dice.Roll(expression);

            Assert.That(roll.IsValid(), Is.True, roll.CurrentRollExpression);
            Assert.That(roll.AsSum<double>(), Is.InRange(minimum, maximum), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMinimum<double>(), Is.EqualTo(minimum), roll.CurrentRollExpression);
            Assert.That(roll.AsPotentialMaximum<double>(), Is.EqualTo(maximum), roll.CurrentRollExpression);
            //HACK: We are ignoring the average, since chained rolls can produce decimal rolls during evaluation (#109)
            AssertTrueOrFalse(roll);

            var rolls = roll.AsIndividualRolls<double>();
            Assert.That(rolls.Count(), Is.InRange(minimumRolls, maximumRolls), roll.CurrentRollExpression);
            Assert.That(rolls.Sum(), Is.InRange(minimum, maximum), roll.CurrentRollExpression);
        }

        private void AssertTrueOrFalse(PartialRoll roll)
        {
            var percentageThreshold = random.NextDouble();
            var rollThreshold = random.Next();

            Assert.That(() => roll.AsTrueOrFalse(percentageThreshold), Throws.Nothing, $"Percentage ({percentageThreshold}): {roll.CurrentRollExpression}");
            Assert.That(() => roll.AsTrueOrFalse(rollThreshold), Throws.Nothing, $"Roll ({rollThreshold}): {roll.CurrentRollExpression}");
        }
    }
}

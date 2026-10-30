using DnDGen.RollGen.Tests.Integration.TestData;
using NUnit.Framework;

namespace DnDGen.RollGen.Tests.Integration
{
    [TestFixture]
    public class IsValidTests : IntegrationTests
    {
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
        }

        [TestCaseSource(typeof(ExpressionTestData), nameof(ExpressionTestData.ValidityTestCases))]
        public void IsValid_Expression(string rollExpression, bool expected)
        {
            var actual = dice.IsValid(rollExpression);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1, 1, true)]
        [TestCase(-1, 1, false)]
        [TestCase(1, -1, false)]
        [TestCase(-1, -1, false)]
        [TestCase(Limits.Quantity, 1, true)]
        [TestCase(Limits.Quantity + 1, 1, false)]
        [TestCase(1, Limits.Die, true)]
        [TestCase(1, Limits.Die + 1, false)]
        [TestCase(Limits.Quantity, Limits.Die, true)]
        [TestCase(Limits.Quantity + 1, Limits.Die, false)]
        [TestCase(Limits.Quantity, Limits.Die + 1, false)]
        [TestCase(Limits.Quantity + 1, Limits.Die + 1, false)]
        public void IsValid_Numeric(int quantity, int die, bool expected)
        {
            var actual = dice.Roll(quantity).d(die).IsValid();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}

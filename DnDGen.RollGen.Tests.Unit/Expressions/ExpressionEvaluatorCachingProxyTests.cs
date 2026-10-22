using DnDGen.RollGen.Expressions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.RollGen.Tests.Unit.Expressions
{
    [TestFixture]
    public class ExpressionEvaluatorCachingProxyTests
    {
        private ExpressionEvaluator proxy;
        private Mock<ExpressionEvaluator> mockInnerEvaluator;

        [SetUp]
        public void Setup()
        {
            mockInnerEvaluator = new Mock<ExpressionEvaluator>();
            proxy = new ExpressionEvaluatorCachingProxy(mockInnerEvaluator.Object);

            mockInnerEvaluator.Setup(e => e.Evaluate<int>("9266+90210")).Returns(99476);
        }

        [Test]
        public void Evaluate_ReturnsInnerResult()
        {
            var result = proxy.Evaluate<int>("9266+90210");
            Assert.That(result, Is.EqualTo(99476));
        }

        [Test]
        public void Evaluate_CachesResult()
        {
            proxy.Evaluate<int>("9266+90210");
            var result = proxy.Evaluate<int>("9266+90210");

            Assert.That(result, Is.EqualTo(99476));
            mockInnerEvaluator.Verify(e => e.Evaluate<int>("9266+90210"), Times.Once);
        }

        [Test]
        public void Evaluate_DoesNotShareCache_BetweenTypes()
        {
            mockInnerEvaluator.Setup(e => e.Evaluate<double>("9266+90210")).Returns(99476.42);

            var intResult = proxy.Evaluate<int>("9266+90210");
            var doubleResult = proxy.Evaluate<double>("9266+90210");

            Assert.That(intResult, Is.EqualTo(99476));
            Assert.That(doubleResult, Is.EqualTo(99476.42));
            mockInnerEvaluator.Verify(e => e.Evaluate<int>("9266+90210"), Times.Once);
            mockInnerEvaluator.Verify(e => e.Evaluate<double>("9266+90210"), Times.Once);
        }

        [Test]
        public void Evaluate_DoesNotCacheDifferentExpression()
        {
            mockInnerEvaluator.Setup(e => e.Evaluate<int>("42*600")).Returns(25200);

            var result = proxy.Evaluate<int>("9266+90210");
            var otherResult = proxy.Evaluate<int>("42*600");

            Assert.That(result, Is.EqualTo(99476));
            Assert.That(otherResult, Is.EqualTo(25200));
            mockInnerEvaluator.Verify(e => e.Evaluate<int>("9266+90210"), Times.Once);
            mockInnerEvaluator.Verify(e => e.Evaluate<int>("42*600"), Times.Once);
        }

        [Test]
        public void Evaluate_DoesNotCacheExceptions()
        {
            var exception = new InvalidOperationException("I failed");
            mockInnerEvaluator.SetupSequence(e => e.Evaluate<int>("1337/0"))
                .Throws(exception)
                .Returns(1336);

            Assert.That(() => proxy.Evaluate<int>("1337/0"), Throws.InvalidOperationException.And.EqualTo(exception));

            var result = proxy.Evaluate<int>("1337/0");
            Assert.That(result, Is.EqualTo(1336));
            mockInnerEvaluator.Verify(e => e.Evaluate<int>("1337/0"), Times.Exactly(2));
        }

        [Test]
        public async Task Evaluate_CachesResult_WhenCalledConcurrently()
        {
            var tasks = new List<Task<int>>();
            while (tasks.Count < 10)
            {
                var task = Task.Run(() => proxy.Evaluate<int>("9266+90210"));
                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);

            Assert.That(results, Is.All.EqualTo(99476));
            mockInnerEvaluator.Verify(e => e.Evaluate<int>("9266+90210"), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsValid_ReturnsInnerResult(bool valid)
        {
            mockInnerEvaluator.Setup(e => e.IsValid("9266+90210")).Returns(valid);

            var result = proxy.IsValid("9266+90210");
            Assert.That(result, Is.EqualTo(valid));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsValid_CachesResult(bool valid)
        {
            mockInnerEvaluator.Setup(e => e.IsValid("9266+90210")).Returns(valid);

            proxy.IsValid("9266+90210");
            var result = proxy.IsValid("9266+90210");

            Assert.That(result, Is.EqualTo(valid));
            mockInnerEvaluator.Verify(e => e.IsValid("9266+90210"), Times.Once);
        }

        [Test]
        public void IsValid_DoesNotCacheDifferentExpression()
        {
            mockInnerEvaluator.Setup(e => e.IsValid("9266+90210")).Returns(true);
            mockInnerEvaluator.Setup(e => e.IsValid("bad(9266)")).Returns(false);

            var result = proxy.IsValid("9266+90210");
            var otherResult = proxy.IsValid("bad(9266)");

            Assert.That(result, Is.True);
            Assert.That(otherResult, Is.False);
            mockInnerEvaluator.Verify(e => e.IsValid("9266+90210"), Times.Once);
            mockInnerEvaluator.Verify(e => e.IsValid("bad(9266)"), Times.Once);
        }
    }
}

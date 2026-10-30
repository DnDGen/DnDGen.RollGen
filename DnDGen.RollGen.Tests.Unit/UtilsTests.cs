using NUnit.Framework;

namespace DnDGen.RollGen.Tests.Unit
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void ChangeType_FromString_ToString()
        {
            var result = Utils.ChangeType<string>("my string");
            Assert.That(result, Is.EqualTo("my string").And.TypeOf<string>());
        }

        [Test]
        public void ChangeType_FromString_ToDouble()
        {
            var result = Utils.ChangeType<double>("90.21");
            Assert.That(result, Is.EqualTo(90.21).And.TypeOf<double>());
        }

        [Test]
        public void ChangeType_FromString_ToInt()
        {
            var result = Utils.ChangeType<int>("42");
            Assert.That(result, Is.EqualTo(42).And.TypeOf<int>());
        }

        [TestCase("True", true)]
        [TestCase("False", false)]
        public void ChangeType_FromString_ToBoolean(string input, bool expected)
        {
            var result = Utils.ChangeType<bool>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<bool>());
        }

        [Test]
        public void ChangeType_FromDouble_ToString()
        {
            var result = Utils.ChangeType<string>(90.21);
            Assert.That(result, Is.EqualTo("90.21").And.TypeOf<string>());
        }

        [Test]
        public void ChangeType_FromDouble_ToDouble()
        {
            var result = Utils.ChangeType<double>(90.21);
            Assert.That(result, Is.EqualTo(90.21).And.TypeOf<double>());
        }

        [Test]
        public void ChangeType_FromDouble_ToInt()
        {
            var result = Utils.ChangeType<int>(90.21);
            Assert.That(result, Is.EqualTo(90).And.TypeOf<int>());
        }

        [TestCase(-10, true)]
        [TestCase(-1, true)]
        [TestCase(-0.5, true)]
        [TestCase(0, false)]
        [TestCase(0.5, true)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void ChangeType_FromDouble_ToBoolean(double input, bool expected)
        {
            var result = Utils.ChangeType<bool>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<bool>());
        }

        [Test]
        public void ChangeType_FromInt_ToString()
        {
            var result = Utils.ChangeType<string>(42);
            Assert.That(result, Is.EqualTo("42").And.TypeOf<string>());
        }

        [Test]
        public void ChangeType_FromInt_ToDouble()
        {
            var result = Utils.ChangeType<double>(42);
            Assert.That(result, Is.EqualTo(42).And.TypeOf<double>());
        }

        [Test]
        public void ChangeType_FromInt_ToInt()
        {
            var result = Utils.ChangeType<int>(42);
            Assert.That(result, Is.EqualTo(42).And.TypeOf<int>());
        }

        [TestCase(-10, true)]
        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void ChangeType_FromInt_ToBoolean(int input, bool expected)
        {
            var result = Utils.ChangeType<bool>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<bool>());
        }

        [TestCase(true, "True")]
        [TestCase(false, "False")]
        public void ChangeType_FromBool_ToString(bool input, string expected)
        {
            var result = Utils.ChangeType<string>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<string>());
        }

        [TestCase(true, 1)]
        [TestCase(false, 0)]
        public void ChangeType_FromBool_ToDouble(bool input, double expected)
        {
            var result = Utils.ChangeType<double>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<double>());
        }

        [TestCase(true, 1)]
        [TestCase(false, 0)]
        public void ChangeType_FromBool_ToInt(bool input, int expected)
        {
            var result = Utils.ChangeType<int>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<int>());
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void ChangeType_FromBool_ToBoolean(bool input, bool expected)
        {
            var result = Utils.ChangeType<bool>(input);
            Assert.That(result, Is.EqualTo(expected).And.TypeOf<bool>());
        }
    }
}

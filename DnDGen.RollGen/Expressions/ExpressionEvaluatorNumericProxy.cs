using System.Globalization;
using System.Linq;
using System.Numerics;

namespace DnDGen.RollGen.Expressions
{
    internal class ExpressionEvaluatorNumericProxy(ExpressionEvaluator innerEvaluator) : ExpressionEvaluator
    {
        private const NumberStyles Number = NumberStyles.Float;

        public T Evaluate<T>(string expression)
        {
            if (!NumericType<T>.IsNumeric || !double.TryParse(expression, Number, CultureInfo.InvariantCulture, out var number))
                return innerEvaluator.Evaluate<T>(expression);

            return Utils.ChangeType<T>(number);
        }

        public bool IsValid(string expression)
        {
            if (double.TryParse(expression, Number, CultureInfo.InvariantCulture, out _))
                return true;

            return innerEvaluator.IsValid(expression);
        }

        private static class NumericType<T>
        {
            public static readonly bool IsNumeric = typeof(T).GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INumberBase<>));
        }
    }
}
